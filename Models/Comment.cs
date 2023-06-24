using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class Comment
    {
        public int Id { get; set; }


        [Display(Name = "امتیاز")]
        public int Rate { get; set; }


        [Display(Name = "متن نظر")]
        public string Text { get; set; }
        
        
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }


        public bool IsActive { get; set; }


        public int UserId { get; set; }


        public int ProductId { get; set; }







        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}