using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace BaziMarket2.Models
{
    public class Discount
    {
        public int Id { get; set; }


        public string Code { get; set; }


        public double Percent { get; set; }



        public int MaxPrice { get; set; }




        public virtual Category Category { get; set; }

    }
}