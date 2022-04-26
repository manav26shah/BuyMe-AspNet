using System.Text.Json.Serialization;

namespace BuyMe.API.DTO.Response
{
    public class CartResponse
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("userId")]
        public string Email { get; set; }
    }
}
