using System;

namespace ProductPriceStatistics.Domain.Services
{
    public interface IAddPriceToProductService
    {
        public void AddPriceToProduct(string productName, decimal price, string storeName, DateTime dateTimeStamp);
    }
}
