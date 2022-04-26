using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BuyMe.DL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal MRPAmount { get; set; }
        public int DiscountPercentage { get; set; }
        public bool InStock { get; set; }
        public int MaxOrderAmount { get; set; }
        public string InventoryId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
