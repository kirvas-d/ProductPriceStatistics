using System;
using System.Text.Json.Serialization;

namespace ProductWebApi.Models
{
    public class Price
    {
        public long PriceId { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTimeMeassure { get; set; }

        [JsonIgnore]
        public long ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public long ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
