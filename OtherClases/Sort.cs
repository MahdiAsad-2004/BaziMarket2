using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaziMarket2.OtherClases
{
    public class Sort
    {
        public int Id { get; set; } 

        public string Name { get; set; }




        public Sort()
        {

        }
        public Sort(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }


        public static List<Sort> GetSorts()
        {
            return new List<Sort>()
            {
                new Sort() { Id = 1, Name = "ارزان ترین" },
                new Sort() { Id = 2, Name = "گران ترین" },
                new Sort() { Id = 3, Name = "پرفروش ترین" },
                new Sort() { Id = 4, Name = "جدید ترین" },
                new Sort() { Id = 5, Name = "بیشترین تخفیف" }
            };
        }


        
    }
}