namespace ProductPriceStatistics.WebApi
{
    public class DbContextConfiguration
    { 
            public const string ConfigurationKey = "DbConnectionString";

            public string ConnectionString { get; set; }
    }
}
