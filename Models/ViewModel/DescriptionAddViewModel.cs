using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BaziMarket2.Models.ViewModel
{
    public class DescriptionAddViewModel
    {
        [Display(Name = "عنوان توضیح")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [StringLength(30, ErrorMessage = "عنوان باید حداقل 2 و حداکثر 30 کاراکتر باشد",MinimumLength = 2)]
        public string Title { get; set; }




        [Display(Name = "متن توضیح")]
        [StringLength(2000, ErrorMessage = "متن باید حداکثر 2000 کاراکتر باشد")]
        public string Text { get; set; }


        public int? Index { get; set; }



        public int? ProductId { get; set; }





    }
}