using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models.ViewModel
{
    public class CategoryAddViewModel
    {
        public int Id { get; set; }


        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = " {0} ضروری است !")]
        [StringLength(30,ErrorMessage = "{0} باید حداکثر 30 کاراکتر داشته باشد!")]
        public string Name { get; set; }



        public int? ParentId { get; set; }
    }
}