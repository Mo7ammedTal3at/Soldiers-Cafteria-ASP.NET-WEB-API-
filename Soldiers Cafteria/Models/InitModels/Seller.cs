using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Seller
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(255)]
        [Required]

        public string Name { get; set; }
        [Index(IsUnique = true)]
        [StringLength(13)]
        [Required]

        public string MilitaryNumber { get; set; }
        [Index(IsUnique =true)]
        [StringLength(13)]
        [Required(ErrorMessage ="the username is required")]

        public string Username { get; set; }
        [Required(ErrorMessage = "the username is required")]

        public string Password { get; set; }

        //public virtual List<Product> Products { get; set; }
        //public virtual List<SellerPersonOrder> SellerPersonOrders { get; set; }
        public virtual List<Order> Orders { get; set; }
        //public List<Payment> Payments { get; set; }

    }
}