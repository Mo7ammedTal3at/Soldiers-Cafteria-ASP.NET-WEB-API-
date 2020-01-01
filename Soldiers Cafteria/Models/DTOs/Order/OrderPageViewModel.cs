using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.DTOs.Order
{
    public class PersonDropDown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Ta2re4a { get; set; }
    }
    public class ProductDropDown
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
    public class OrderPageDTO
    {
        public List<PersonDropDown> People { get; set; }
        public List<ProductDropDown> Products { get; set; }
    }
}