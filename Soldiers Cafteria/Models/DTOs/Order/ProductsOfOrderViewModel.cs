using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Order
{
    public class ProductsOfOrderDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float SellPrice { get; set; }
        [Required]
        public float TotalPrice { get; set; }
        [Required]
        public int CountOfProduct { get; set; }
    }
}