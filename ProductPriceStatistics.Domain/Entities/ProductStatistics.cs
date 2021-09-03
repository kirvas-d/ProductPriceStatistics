using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Entities
{
    public class ProductStatistics
    {
        private Product _product;
        private List<Price> _prices;

        public Guid Id => _product.Id;

        public string Name => _product.Name;

        public IReadOnlyCollection<Tag> Tags => _product.Tags;

        public IReadOnlyCollection<Price> Prices => _prices;

        public ProductStatistics(Product product, IEnumerable<Price> prices) 
        {
            _product = product;
            _prices = new List<Price>(prices);
        }
    }
}
