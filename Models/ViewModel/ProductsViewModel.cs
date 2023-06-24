using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class ProductsViewModel
    {
        public string Name { get; set; }


        public int Price { get; set; }


        public int InStockCount { get; set; }


        public double? Discount { get; set; }


        public string Image { get; set; }



        public int GetPrice()
        {
            if (Discount == null)
            {
                return Price;
            }
            return (int)(Price - (Price * Discount / 100));
        }

        public string EncodeName()
        {
            return HttpUtility.UrlEncode(this.Name, Encoding.UTF8).Replace("+", "-");
        }


        public string DecodeName()
        {
            return HttpUtility.UrlDecode(this.Name, Encoding.UTF8).Replace("+", "-");
        }
    }
}
