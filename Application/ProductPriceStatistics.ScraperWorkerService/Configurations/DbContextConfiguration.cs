namespace ProductPriceStatistics.ScraperWorkerService.Configurations
{
    class DbContextConfiguration
    {
        public const string ConfigurationKey = "DbConnectionString";

        public string ConnectionString { get; set; }
    }
}
