using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class Picture
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public int Index { get; set; }


        public int? ProductId { get; set; }



        public virtual Product Product { get; set; }
    }
}