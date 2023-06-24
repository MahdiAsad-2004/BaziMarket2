using BaziMarket2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class Question
    {
        public int Id { get; set; }


        public string Text { get; set; }


        public DateTime RegisterDate { get; set; }



        public bool IsActive { get; set; }


        public int ProductId { get; set; }
        
        
        public int? UserId { get; set; }



        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual Answer Answer { get; set; }






        public string GetName()
        {
            return string.Format($"{this.User.FirstName} {User.LastName}");
        }

    }
}