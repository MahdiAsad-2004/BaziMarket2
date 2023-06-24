using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BaziMarket2.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int Cost { get; set; } = 0;




        public virtual User User { get; set; }
        public virtual List<ProductCart> ProductCarts { get; set; } 
    }
}