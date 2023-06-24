using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class Answer
    {
        public int Id { get; set; }


        public string Text { get; set; }


        public DateTime RegisterDate { get; set; }


        public bool IsActive { get; set; }


        public int? UserId { get; set; }



        public virtual User User { get; set; }

        public virtual Question Question { get; set; }  
    
    
    
    
    
    
    
        public string GetUserName()
        {
            return string.Format($"{this.User.FirstName} {User.LastName}");
        }
    }

}