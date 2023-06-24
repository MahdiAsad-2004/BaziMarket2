using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using BaziMarket2.Validators;
using System.ComponentModel.DataAnnotations;

namespace BaziMarket2.Models.ViewModel
{
    public class TestModel2
    {
        [Required(ErrorMessage = "its required")]
        [StringLength(5,ErrorMessage = "max length is 5")]
        public string Name { get; set; }



        [Required(ErrorMessage = "This is required !")]
        [MaxFileSize(_maxFileSize = 500000, ErrorMessage = "حجم عکس بیش از 500 KB است!")]
        public HttpPostedFileBase Image { get; set; }
    }
}