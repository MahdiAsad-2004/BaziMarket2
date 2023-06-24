using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class ProductCart
    {
        public int Id { get; set; }
        public int Number { get; set; } = 0;
        public int? ProductId { get; set; }
        public int? CartId { get; set; }



        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}