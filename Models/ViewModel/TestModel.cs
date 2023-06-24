using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BaziMarket2.Models.ViewModel
{
    public class TestModel
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "نام کاربری ضروری است !")]
        [RegularExpression("09[0-9]{9}", ErrorMessage = "فرمت نام کاربری اشتباه است !")]
        public string Username { get; set; }



        [Required(ErrorMessage = "رمز عبور ضروری است !")]
        [StringLength(16, ErrorMessage = "رمز عبور باید حداقل 8 و حداکثر 16 حرف باشد !", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
