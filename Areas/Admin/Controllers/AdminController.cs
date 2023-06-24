using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaziMarket2.Models;

namespace BaziMarket2.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();

        //[Route("Admin/AdminPanel")]
        public ActionResult Panel()
        {
            float a = db_BaziMarket.T_Comment.Sum(t => t.Rate) / db_BaziMarket.T_Comment.Count() * 20;
            ViewBag.RatesAvg = a;
            ViewBag.UsersCount = db_BaziMarket.T_User.Count();
            ViewBag.ProductsCount = db_BaziMarket.T_Product.Count();
            ViewData["LastProducts"] = db_BaziMarket.T_Product.OrderByDescending(t => t.RegisterDate).Take(5).ToList();
            ViewData["LastUsers"] = db_BaziMarket.T_User.OrderByDescending(t => t.RegisterDate).Take(8).ToList();
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            return View("");
        }
    }
}