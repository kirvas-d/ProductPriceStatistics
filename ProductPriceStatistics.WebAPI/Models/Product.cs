using System.Collections.Generic;

namespace ProductWebApi.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }

        public List<Price> Prices { get; set; } = new List<Price>();
    }
}
