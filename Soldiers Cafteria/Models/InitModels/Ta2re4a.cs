using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Soldiers_Cafteria.Models.InitModels
{
    public class Ta2re4a
    {
        public int Id { get; set; }
        [Required]

        public float MaxValue { get; set; }
        public float CurrentValue { get; set; } = 0F;
        public DateTime AddtionTime { get; set; }
        //[Required]
        //public int PersonId { get; set; }
        //[ForeignKey("PersonId")]
        //public Person Person { get; set; }
        public bool IsOpen { get; set; }= true;


    }
}