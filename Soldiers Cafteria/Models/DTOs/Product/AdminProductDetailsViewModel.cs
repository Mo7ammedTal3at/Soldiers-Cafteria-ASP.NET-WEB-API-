using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Product
{
    public class AdminProductDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SellPrice { get; set; }// the sell price for one item only
        public float BuyPrice { get; set; }// the Buy price for one item only
        public int NumberOfBoxes { get; set; }
        public int NumberOfItemsInBox { get; set; }
        public int NumberOfRestItems { get; set; } = 0;// the remaining number of items that not in box (future update)
        public int AlertLimit { get; set; }
        public string AddtionDate { get; set; }
        public bool IsLimited { get; set; } = false;
    }
}