using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models.ViewModel
{
    public class PropertyAddViewModel
    {

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [StringLength(30, ErrorMessage = "{0} باید حداکثر 30 کارارکتر باشد")]
        public string Text { get; set; }


        public int? ProductId { get; set; }


        public int Index { get; set; }

    }
}