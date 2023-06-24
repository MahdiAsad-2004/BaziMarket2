using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class UserListViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsActive { get; set; }

        public string ProfileImage { get; set; }

        public string Address { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; } 


    }
}