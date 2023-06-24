using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace BaziMarket2.Models
{
    public class Product
    {
        public int Id { get; set; }


       
        public string Name { get; set; }



        public int Price { get; set; }



        public int InStockCount { get; set; }


        
        public int Sold { get; set; } = 0;


     
        public bool IsActive { get; set; }
        
        
        public DateTime RegisterDate { get; set; }
        
        
        public double? Discount { get; set; }



        public int? CategoryId { get; set; }


        public string Image { get; set; }





        public virtual Category Category { get; set; }
        public virtual List<Description> Descriptions { get; set; }
        public virtual List<Specification> Specifications { get; set; }
        public virtual List<Property> Properties { get; set; }
        public virtual List<ProductCart> ProductCarts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<Picture> Pictures { get; set; }
        public virtual List<User> Users { get; set; }





        public int GetPrice()
        {
            if (Discount == null)
            {
                return Price;
            }
            return (int)(Price - (Price * Discount / 100));
        }

        public float CalculateRate(out int RateInt)
        {
            RateInt = 0;
            if (this.Comments == null)
            {
                return -1;
            }
            float rates = (float)((float)Comments.Where(t => t.IsActive).Sum(t => t.Rate) / (float)Comments.Count);
            RateInt = (int)rates;
            return (rates*10)%10 ; 
        }


        public string EncodeName()
        {
            return HttpUtility.UrlEncode(this.Name,Encoding.UTF8).Replace("+","-");
        }


        public string DecodeName()
        {
            return HttpUtility.UrlDecode(this.Name, Encoding.UTF8).Replace("+", "-");
        }
    }
}