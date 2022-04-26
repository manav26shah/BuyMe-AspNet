using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BuyMe.DL.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }    
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("AspNetUsers")]
        public string Email { get; set; }
    }
}
