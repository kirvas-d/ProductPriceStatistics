using System;

namespace ProductPriceStatistics.Core.Commands
{
    public class AddPriceToProductCommand
    {
        public string ProductName { get; init; }
        public decimal Price { get; init; }
        public string StoreName { get; init; }
        public DateTime DateTimeStamp { get; init; }

        public AddPriceToProductCommand(string productName, decimal price, string storeName, DateTime dateTimeStamp)
        {
            if (productName == null) throw new ArgumentNullException($"{nameof(productName)} mustn't be null");
            ProductName = productName;

            if (price == default(decimal)) throw new ArgumentException($"{nameof(price)} mustn't be default value");
            Price = price;

            if (storeName == null) throw new ArgumentNullException($"{nameof(storeName)} mustn't be null");
            StoreName = storeName;

            if (dateTimeStamp == default(DateTime)) throw new ArgumentException($"{nameof(dateTimeStamp)} mustn't be default value");
            DateTimeStamp = dateTimeStamp;
        }
    }
}
