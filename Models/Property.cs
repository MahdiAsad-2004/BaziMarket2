using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models
{
    public class Property
    {
        public int Id { get; set; }


        public string Text { get; set; }


        public int? ProductId { get; set; }


        public int Index { get; set; }


        public virtual Product Product { get; set; }
    }
}