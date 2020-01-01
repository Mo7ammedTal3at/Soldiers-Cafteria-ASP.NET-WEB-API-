using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Product
{
    public class SellerProductDetailsDTO
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public float SellPrice { get; set; }
        [Required]
        public int NumberByBox { get; set; }
        public int AlertLimit { get; set; }
        public bool IsLimited { get; set; } = false;
        public string CategoryName { get; set; }
    }
}