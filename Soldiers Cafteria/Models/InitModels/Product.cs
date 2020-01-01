using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Product
    {  
        public int Id { get; set; }
        [Index(IsUnique =true)]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public float SellPrice { get; set; }
        [Required]
        public float BuyPrice { get; set; }

        [Required]
        public int TotalItemsCount { get; set; }
        [Required]
        public int NumberOfItemsInBox { get; set; }
        public int AlertLimit { get; set; }
        public DateTime AddtionTime { get; set; }
        public bool IsLimited { get; set; } = false;
        //public virtual Order Order { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }

    }
}