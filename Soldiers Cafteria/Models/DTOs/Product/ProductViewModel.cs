using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Product
{
    public class ProductDTO
    {
        public int SellerId { get; set; }
        [Index(IsUnique = true)]
        [StringLength(255)]
        [Required(ErrorMessage = "لابد من ادخال اسم المنتج")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لابد من ادخال سعر البيع")]
        public float SellPrice { get; set; }
        [Required(ErrorMessage = "لابد من ادخال اسم الجملة")]
        public float BuyPrice { get; set; }
        [Required(ErrorMessage = "لابد من ادخال عدد الكراتين")]
        public int NumberOfBoxes { get; set; }
        [Required]
        public int NumberOfItemsInBox { get; set; }
        public int AlertLimit { get; set; }
        public int CategoryId { get; set; }

    }
}