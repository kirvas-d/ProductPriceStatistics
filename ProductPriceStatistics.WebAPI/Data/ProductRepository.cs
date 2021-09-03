using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductStoreMicroservice.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsContext _productsContext;

        public ProductRepository(ProductsContext productsContext)
        {
            _productsContext = productsContext;
        }

        public Product GetProduct(int productId)
        {
            return _productsContext.Products.Include(p => p.Prices).FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productsContext.Products.ToList();
        }
    }
}
