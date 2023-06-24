using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class QuestionEditViewModel
    {
        public int Id { get; set; }


        public string Text { get; set; }


        public DateTime RegisterDate { get; set; }


        public bool IsActive { get; set; }



        public virtual User User { get; set; }

        public virtual Answer Answer { get; set; }
    }
}