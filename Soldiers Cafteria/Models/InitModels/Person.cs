using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Soldiers_Cafteria.Models.InitModels
{
    public class Person
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(255)]
        [Required(ErrorMessage ="أدخل الاسم قبل ارسال البيانات")]
        public string Name { get; set; }
        //[Index(IsUnique =true)]
        //[StringLength(13,ErrorMessage ="الرقم العسكرى غير صحيح لابد ان يكون 13 رقم")]
        [Required]
        public string MilitaryNumber { get; set; }
        [Required(ErrorMessage = "أدخل درجة الشخص ( ضابط , صف ضابط , عسكرى")]
        public int DaragaId { get; set; }
        
        [Required(ErrorMessage = "أدخل اسم الفرع")]
        public int Far3Id { get; set; }
        
        public DateTime AddtionTime { get; set; }
        public int Ta2re4aId { get; set; }
        [ForeignKey("Ta2re4aId")]
        public virtual Ta2re4a Ta2Re4A { get; set; }
        public virtual List<Order> Orders { get; set; }
        //public virtual List<Payment> Payments { get; set; }
        [ForeignKey("DaragaId")]
        public virtual Daraga Daraga { get; set; }
        [ForeignKey("Far3Id")]
        public virtual Far3 Far3 { get; set; }


    }
}