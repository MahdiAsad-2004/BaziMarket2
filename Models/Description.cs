using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace BaziMarket2.Models
{
    public class Description
    {
        public int Id { get; set; }


        public string Title { get; set; }

        
        public string Text { get; set; }
        


        public string Image { get; set; }



        public int? ProductId { get; set; }



        public int Index { get; set; }






        public virtual Product Product { get; set; }


        

    }
}