using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using BaziMarket2.Validators;



namespace BaziMarket2.Models.ViewModel
{
    public class UserRegisterViewModel
    {

        [Required(ErrorMessage = "نام کاربری ضروری است !")]
        [RegularExpression("09[0-9]{9}",ErrorMessage = "فرمت نام کاربری اشتباه است !")]
        public string Username { get; set; }



        [Required(ErrorMessage = "رمز عبور ضروری است !")]
        [StringLength(16,ErrorMessage = "رمز عبور باید حداقل 8 و حداکثر 16 حرف باشد !",MinimumLength = 8)]
        public string Password { get; set; }



        [Required(ErrorMessage = "تکرار رمز عبور ضروری است !")]
        [Compare("Password",ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند !")]
        public string RepeatPassword { get; set; }



        [Required(ErrorMessage = "نام ضروری است !")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$",ErrorMessage = "نام باید فارسی باشد !")]
        [StringLength(30, ErrorMessage = "نام نمیتواند کمتر از 2 و بیشتر از 30 کاراکتر داشته باشد!", MinimumLength = 2)]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "نام خانوادگی ضروری است !")]
        [RegularExpression("[\\u0600-\\u06FF\\s]+$", ErrorMessage = "نام خانوادگی باید فارسی باشد !")]
        [StringLength(30, ErrorMessage = "نام خانوادگی نمیتواند کمتر از 2 و بیشتر از 30 کاراکتر داشته باشد!", MinimumLength = 2)]
        public string LastName { get; set; }

        
        public bool IsActive { get; set; }



        [StringLength(200, ErrorMessage = "آدرس نمیتواند بیشتر از 200 کاراکتر داشته باشد!")]
        public string Address { get; set; }
        


        public int? RoleId { get; set; }



    }
}