using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class PaymentOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}