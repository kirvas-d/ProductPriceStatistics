using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPriceStatistics.Domain.Services
{
    public interface IAddPriceToProductService
    {
        public void AddPriceToProduct(string productName, Price price);
    }
}
