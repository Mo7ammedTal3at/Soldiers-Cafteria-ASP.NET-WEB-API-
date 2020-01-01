using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Payment
{
    public class PaymentDTO
    {
        public float Value { get; set; }
        public int PaymentOptionId { get; set; }
        public DateTime Time { get; set; }
    }
}