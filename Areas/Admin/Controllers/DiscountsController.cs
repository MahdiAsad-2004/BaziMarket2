using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaziMarket2.App_Start;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;

namespace BaziMarket2.Areas.Admin.Controllers
{
    public class DiscountsController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();


        public ActionResult AddDiscount()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            return View(db_BaziMarket.T_Discount.ToList());
        }
        [HttpPost]
        public ActionResult AddDiscount(DiscountAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            Discount discount = AutoMapperConfig.mapper.Map<DiscountAddViewModel, Discount>(viewModel);
            db_BaziMarket.T_Discount.Add(discount);
            db_BaziMarket.SaveChanges();
            return Json(new
            {
                message = $"کد تخفیف ( {discount.Code} ) با مفقیت افزوده شد",
                action = Url.Action("AddDiscount","Discounts",null,Request.Url.Scheme)
            });
        }

        [HttpPost]
        public ActionResult DeleteDiscount(int? discountId)
        {
            Discount discount = db_BaziMarket.T_Discount.Find(discountId);
            if (discount == null)
            {
                return Content("false");
            }
            db_BaziMarket.T_Discount.Remove(discount);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("AddDiscount","Discounts");
        }
    }
}