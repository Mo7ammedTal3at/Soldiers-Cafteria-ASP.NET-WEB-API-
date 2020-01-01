using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Soldiers_Cafteria.Models.InitModels;

namespace Soldiers_Cafteria.Models.DTOs.Person
{
    public class PersonDTO
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        [Required(ErrorMessage = "أدخل الاسم قبل ارسال البيانات")]
        public string Name { get; set; }
        [Index(IsUnique = true)]
        [StringLength(13, ErrorMessage = "الرقم العسكرى غير صحيح لابد ان يكون 13 رقم")]
        [Required]
        public string MilitaryNumber { get; set; }
        [Required(ErrorMessage = "أدخل درجة الشخص ( ضابط , صف ضابط , عسكرى")]
        public int DaragaId { get; set; }
        [Required(ErrorMessage = "أدخل اسم الفرع")]
        public int Far3Id { get; set; }
        public float Ta2re4aMaxValue { get; set; }
    }
}