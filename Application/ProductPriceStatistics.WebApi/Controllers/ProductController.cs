using Microsoft.AspNetCore.Mvc;
using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.QueryHandlers;
using System.Collections.Generic;

namespace ProductPriceStatistics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryHandler<IEnumerable<ProductDto>> _getAllProductQueryHandler;

        public ProductController(IQueryHandler<IEnumerable<ProductDto>> getAllProductsQueryHandler) 
        {
            _getAllProductQueryHandler = getAllProductsQueryHandler;
        }

        // GET: api/products
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            return _getAllProductQueryHandler.Ask();
        }
    }
}
