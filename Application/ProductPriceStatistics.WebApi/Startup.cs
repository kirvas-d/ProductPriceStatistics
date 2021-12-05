using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductPriceStatistics.Core.CommandHandlers;
using ProductPriceStatistics.Core.Commands;
using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.Queries;
using ProductPriceStatistics.Core.QueryHandlers;
using ProductPriceStatistics.Core.ReadRepositories;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using ProductPriceStatistics.Infrastructure.RabbitMQService;
using ProductPriceStatistics.WebApi.Services;
using System.Collections.Generic;

namespace ProductPriceStatistics.WebApi
{
    public class Startup
    {
        private readonly string _developmentCors = "DevelopmentPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_developmentCors,
                                  builder =>
                                  {
                                      builder
                                        .AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
            });

            var dbContextConfiguration = new DbContextConfiguration();
            Configuration.GetSection(DbContextConfiguration.ConfigurationKey).Bind(dbContextConfiguration);
            var dbOptions = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseNpgsql(dbContextConfiguration.ConnectionString)
                .Options;

            //var rabbitMQServiceConfiguration = new RabbitMQServiceConfiguration();
            //Configuration.GetSection(RabbitMQServiceConfiguration.ConfigurationKey).Bind(rabbitMQServiceConfiguration);

            //services.AddSingleton(rabbitMQServiceConfiguration);
            services.AddSingleton<ProductPriceStatisticsDbContext>(new ProductPriceStatisticsDbContext(dbOptions));
            services.AddSingleton(typeof(IQueryHandler<GetPricesOfProductQuery, IEnumerable<PriceDto>>), typeof(GetPricesOfProductQueryHandler));
            services.AddSingleton(typeof(IQueryHandler<IEnumerable<ProductDto>>), typeof(GetAllProductsQueryHandler));
            services.AddSingleton(typeof(ICommandHandler<AddPriceToProductCommand>), typeof(AddPriceToProductCommandHandler));
            services.AddSingleton(typeof(IPriceReadRepository), typeof(PriceReadRepository));
            services.AddSingleton(typeof(IProductReadRepository), typeof(ProductReadRepository));
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton(typeof(IPriceRepository), typeof(PriceRepository));
            //services.AddHostedService<AddPriceToProductHandlerHostedService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductPriceStatistics.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_developmentCors);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductPriceStatistics.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
