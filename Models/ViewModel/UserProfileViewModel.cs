using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaziMarket2.Models.ViewModel
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "نام کاربری ضروری است !")]
        [RegularExpression("09[0-9]{9}", ErrorMessage = "فرمت نام کاربری اشتباه است !")]
        public string Username { get; set; }




        [Required(ErrorMessage = "نام ضروری است !")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$", ErrorMessage = "نام باید فارسی باشد !")]
        [StringLength(30, ErrorMessage = "نام نمیتواند کمتر از 2 و بیشتر از 30 کاراکتر داشته باشد!", MinimumLength = 2)]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "نام خانوادگی ضروری است !")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$", ErrorMessage = "نام خانوادگی باید فارسی باشد !")]
        [StringLength(30, ErrorMessage = "نام خانوادگی نمیتواند کمتر از 2 و بیشتر از 30 کاراکتر داشته باشد!", MinimumLength = 2)]
        public string LastName { get; set; }


        public bool IsActive { get; set; }



        public string ProfileImage { get; set; }



        public DateTime RegisterDate { get; set; }



        [StringLength(200, ErrorMessage = "آدرس نمیتواند بیشتر از 200 کاراکتر داشته باشد!")]
        public string Address { get; set; }



    }
}