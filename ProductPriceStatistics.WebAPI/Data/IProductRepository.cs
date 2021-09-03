using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreMicroservice.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProduct(int productId);
    }
}
