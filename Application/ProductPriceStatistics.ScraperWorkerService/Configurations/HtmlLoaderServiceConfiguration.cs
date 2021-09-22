using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPriceStatistics.ScraperWorkerService.Configurations
{
    enum TypeDriver { Remote, Local };

    class HtmlLoaderServiceConfiguration
    {
        public const string ConfigurationKey = "SeleniumWebDriver";

        public TypeDriver TypeDriver { get; set; }

        public string PathToDriver { get; set; }
    }
}
