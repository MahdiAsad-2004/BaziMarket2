using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class ProductAddListViewModel
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int InStockCount { get; set; }

        public string Image { get; set; }

    }
}