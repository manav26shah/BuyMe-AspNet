using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuyMe.API.DTO.Response
{
    public class ProductResponse
    {
        [JsonPropertyName("productId")]
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal MRPAmount { get; set; }
        public int Discount { get; set; }
        public bool InStock { get; set; }
    }
}
