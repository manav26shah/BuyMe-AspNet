using System.Text.Json.Serialization;

namespace BuyMe.API.DTO.Response
{
    public class OrderResponse
    {
        [JsonPropertyName("OrderId")]
        public int Id { get; set; }
        //public string UserID { get; set; }
        public int ProductId { get; set; }
    }
}
