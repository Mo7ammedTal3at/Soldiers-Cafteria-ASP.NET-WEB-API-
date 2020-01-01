using Soldiers_Cafteria.Models.InitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs
{
    public class OrderParameterDTO
    {
        public int SellerId { get; set; }
        public int PersonId { get; set; }
        public string BuyerName { get; set; }
        public List<InitModels.OrderProduct> OrderProductsList { get; set; }
        public PaymentOption PaymentOption { get; set; }
    }
}