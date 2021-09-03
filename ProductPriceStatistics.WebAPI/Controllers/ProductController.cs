using Microsoft.AspNetCore.Mvc;
using ProductStoreMicroservice.Data;
using ProductWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductStoreMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository) 
        {
            this._productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return _productRepository
                       .GetProducts()
                       .Select(p => new { p.ProductId, p.Name });
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}
