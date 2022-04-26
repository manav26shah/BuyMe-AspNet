using System.Collections.Generic;

namespace BuyMe.API.DTO.Response
{
    public class PageResponse
    {
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
        public int Pages { get; set; } 
        public int CurrrentPage { get; set; }
    }
}
