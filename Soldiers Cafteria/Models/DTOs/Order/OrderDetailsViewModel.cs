using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Soldiers_Cafteria.Models.DTOs.Product;

namespace Soldiers_Cafteria.Models.DTOs.Order
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }
        [Required]
        public float TotalPrice { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public string BuyerName { get; set; }
        public string PersonName { get; set; }
        public string SellerName { get; set; }
        public List<ProductsOfOrderDTO> Products { get; set; }
        public List<InitModels.Payment> Payments { get; set; }
    }
}