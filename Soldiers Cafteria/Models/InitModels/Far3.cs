using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Far3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }
    }
}