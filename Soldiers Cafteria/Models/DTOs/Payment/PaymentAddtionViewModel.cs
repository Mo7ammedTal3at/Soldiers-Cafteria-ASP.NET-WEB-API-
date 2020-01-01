using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Payment
{
    public class PaymentAddtionDTO
    {
        public double Value { get; set; }
        public int TagerId { get; set; }
        public int SellerId { get; set; }
    }
}