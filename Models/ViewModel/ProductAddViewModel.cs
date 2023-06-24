using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BaziMarket2.Models.ViewModel
{
    public class ProductAddViewModel
    {

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [StringLength(50, ErrorMessage = "{0} باید حداقل 2 و حداکثر 50 کارارکتر باشد", MinimumLength = 2)]
        public string Name { get; set; }



        [Display(Name = "قیمت")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [Range(0,500000000,ErrorMessage = "{0} باید بین 0 تا 500,000,000 تومان باشد!")]
        public int Price { get; set; }



        [Display(Name = "موجودی")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [Range(0, 1000, ErrorMessage = "{0} باید بین 0 تا 1000 عدد باشد!")]
        public int InStockCount { get; set; }



        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        public bool IsActive { get; set; }



        [Display(Name = "تخفیف")]
        [Range(1, 99, ErrorMessage = "{0} باید بین 1 تا 99 درصد باشد!")]
        public double? Discount { get; set; }



        public int? CategoryId { get; set; }

    }
}