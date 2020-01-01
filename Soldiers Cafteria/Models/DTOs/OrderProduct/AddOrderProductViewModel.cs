using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.OrderProduct
{
    public class AddOrderProductDTO
    {
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        [Required]
        public int CountOfProduct { get; set; }
        ///////////////
        
    }
}