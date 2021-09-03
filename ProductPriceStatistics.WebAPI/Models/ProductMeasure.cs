using System;

namespace ProductStoreMicroservice.Models
{
    public class ProductMeasure
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string StoreName { get; init; }
        public DateTime PriceMeasure {get; init;}

        public ProductMeasure(string name, decimal price, string storeName, DateTime priceMeasure) 
        {
            Name = name;
            Price = price;
            StoreName = storeName;
            PriceMeasure = priceMeasure;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) 
            {
                return false;
            }
            else if (ReferenceEquals(this, obj))
            {
                return true;
            }
            else if(obj is ProductMeasure)
            {
                ProductMeasure product = (ProductMeasure)obj;
                if (this.Name == product.Name) 
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
