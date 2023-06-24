using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BaziMarket2.Models.ViewModel
{
    public class CommentEditViewModel
    {
        public int Id { get; set; }



        public int Rate { get; set; }


        public string Text { get; set; }



        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }


        public bool IsActive { get; set; }




        public virtual User User { get; set; }

    }
}