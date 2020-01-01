using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public float TotalPrice { get; set; } 
        public DateTime Time { get; set; }
        public string BuyerName { get; set; }
        public int PersonId { get; set; }
        public int SellerId { get; set; }
        //public virtual List<Product> Products { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}