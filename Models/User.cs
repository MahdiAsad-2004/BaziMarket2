using BaziMarket2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models
{
    public partial class User
    {

        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime RegisterDate { get; set; }

        public bool IsActive { get; set; }

        public string ProfileImage { get; set; }

        public string Address { get; set; }

        public int? RoleId { get; set; }











        public virtual Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}