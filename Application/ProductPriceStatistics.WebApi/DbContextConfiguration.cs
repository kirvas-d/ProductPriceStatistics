using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductPriceStatistics.WebApi
{
    public class DbContextConfiguration
    { 
            public const string ConfigurationKey = "DbConnectionString";

            public string ConnectionString { get; set; }
    }
}
