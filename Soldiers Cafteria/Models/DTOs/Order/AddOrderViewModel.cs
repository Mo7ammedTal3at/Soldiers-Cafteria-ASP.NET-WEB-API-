using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Soldiers_Cafteria.Models.InitModels;
using Soldiers_Cafteria.Models.DTOs.OrderProduct;

namespace Soldiers_Cafteria.Models.DTOs.Order
{
    public class AddOrderDTO
    {
        public int PersonId { get; set; }
        public int SellerId { get; set; }
        public string BuyerName { get; set; }
        public float TotalPrice { get; set; }
        public float? PriceFromTa2re4a { get; set; }
        public float? PriceFromCash { get; set; }   
        public List<OrderProductVM> OrderProductsVM { get; set; }
        public int PaymentOptionId { get; set; }
    }

    public class OrderProductVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }

}