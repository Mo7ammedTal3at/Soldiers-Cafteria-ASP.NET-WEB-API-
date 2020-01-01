using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Daraga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> People { get; set; }

    }
}