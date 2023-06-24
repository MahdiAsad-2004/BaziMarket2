using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models.ViewModel
{
    public class DiscountAddViewModel
    {
        [Display(Name = "کد تخفیف")]
        [StringLength(30, ErrorMessage = "{0} باید حداقل 3 و حداکثر 30 کاراکتر باشد", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} ضروری است")]
        public string Code { get; set; }


        [Display(Name = "درصد تخفیف")]
        [Range(1, 99, ErrorMessage = "{0} باید بین 1 تا 99 باشد")]
        [Required(ErrorMessage = "{0} ضروری است")]
        public double Percent { get; set; }


        [Display(Name = "حداکثر تخفیف")]
        [Required(ErrorMessage = "{0} ضروری است")]
        [Range(0,50000000, ErrorMessage = "{0} باید بین 0 تا 50,000,000 تومان باشد")]
        public int MaxPrice { get; set; }

    }
}