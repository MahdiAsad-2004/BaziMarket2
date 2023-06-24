using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class UserRegisterListViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public virtual Role Role { get; set; }

        public string GetName()
        {
            return FirstName + " " + LastName;  
        }
      
    }
}