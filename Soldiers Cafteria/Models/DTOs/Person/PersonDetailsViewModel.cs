using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Soldiers_Cafteria.Models.InitModels;


namespace Soldiers_Cafteria.Models.DTOs.Person
{
    public class PersonDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MilitaryNumber { get; set; }
        public string Daraga { get; set; }
        public string Far3{ get; set; }
        public float Ta2re4aMaxValue { get; set; }
        public float Ta2re4aCurrentValue { get; set; }
        public float Ta2re4aRestValue { get; set; }
    }
}