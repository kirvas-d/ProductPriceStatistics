using Microsoft.Extensions.Caching.Memory;
using ProductWebApi.Data;
using ProductWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductStoreMicroservice.Data
{
    public class CachedProductRepository : IProductRepository
    {
        private readonly ProductRepository _productRepository;
        private readonly IMemoryCache _memoryCache;

        public CachedProductRepository(ProductRepository productRepository, IMemoryCache memoryCache) 
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache;
        }

        public Product GetProduct(int productId)
        {
            Product product = null;
            if (!_memoryCache.TryGetValue(productId, out product))
            {
                product = _productRepository.GetProduct(productId);
                _memoryCache.Set(productId, product);
            }

            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
