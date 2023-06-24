using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BaziMarket2.Models.ViewModel
{
    public class UserChangePasswordViewModel
    {
        [Required(ErrorMessage = "نام کاربری ضروری است !")]
        [RegularExpression("09[0-9]{9}", ErrorMessage = "فرمت نام کاربری اشتباه است !")]
        public string Username { get; set; }



        public string Password { get; set; }



        [Required(ErrorMessage = "رمز عبور ضروری است !")]
        [StringLength(16, ErrorMessage = "رمز عبور باید حداقل 8 و حداکثر 16 حرف باشد !", MinimumLength = 8)]
        public string NewPassword { get; set; }



        [Required(ErrorMessage = "تکرار رمز عبور ضروری است !")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند !")]
        public string RepeatNewPassword { get; set; }
    }
}