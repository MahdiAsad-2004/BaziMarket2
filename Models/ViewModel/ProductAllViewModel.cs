using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace BaziMarket2.Models.ViewModel
{
    public class ProductAllViewModel
    {
        public int Id { get; set; }




        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [StringLength(50, ErrorMessage = "{0} باید حداقل 2 و حداکثر 50 کارارکتر باشد", MinimumLength = 2)]
        public string Name { get; set; }



        [Display(Name = "قیمت")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [Range(0, 500000000, ErrorMessage = "{0} باید بین 0 تا 500,000,000 تومان باشد!")]
        public int Price { get; set; }



        [Display(Name = "موجودی")]
        [Required(ErrorMessage = " {0} ضروری است ")]
        [Range(0, 1000, ErrorMessage = "{0} باید بین 0 تا 1000 عدد باشد!")]
        public int InStockCount { get; set; }



        public int Sold { get; set; }



        public bool IsActive { get; set; }



        public DateTime RegisterDate { get; set; }



        [Display(Name = "تخفیف")]
        [Range(1, 99, ErrorMessage = "{0} باید بین 1 تا 99 درصد باشد!")]
        public double? Discount { get; set; }



        public int? CategoryId { get; set; }



        public string Image { get; set; }





        public virtual Category Category { get; set; }






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