using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class Role
    {

        public int Id { get; set; }

        
        [Display(Name = "نقش کاربری")]
        public string Name { get; set; }


        public string Title { get; set; }


        public virtual List<User> Users { get; set; }   
        

    }
}