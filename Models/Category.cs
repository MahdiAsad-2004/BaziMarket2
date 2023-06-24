using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models
{
    public class Category
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string Image { get; set; }


        public int? ParentId { get; set; }






        public virtual Category Category1 { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Discount> Discounts { get; set; }




    }
}