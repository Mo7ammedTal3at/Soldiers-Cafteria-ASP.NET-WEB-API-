using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Soldiers_Cafteria.Models.DTOs.Daraga;
using Soldiers_Cafteria.Models.DTOs.Far3;

namespace Soldiers_Cafteria.Models.DTOs.Person
{
    public class AddPersonVewModel
    {
        public List<DaragaDTO> Daraga { get; set; }
        public List<Far3DTO> Far3 { get; set; }
    }
}