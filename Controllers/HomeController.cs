using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaziMarket2.Models;
using System.Security;
using System.Security.Authentication;
using System.Web.Security;
using BaziMarket2.Models.ViewModel;
using System.Web.WebPages;
using Microsoft.AspNetCore.Http;
using BaziMarket2.App_Start;
using System.IO;

namespace BaziMarket2.Controllers
{
    public class HomeController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();
        // GET: Home
        public ActionResult Home()
        {
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            return View();
        }


        public ActionResult NavBar()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Error404");
                }
                ViewBag.Name = user.FirstName + " " + user.LastName;
                ViewBag.Username = user.Username;
                if (user.Cart != null)
                {
                    int cartCount = 0;
                    cartCount = user.Cart.ProductCarts.Sum(t => t.Number);
                    ViewBag.CartCount = cartCount;
                }
            }
            return PartialView();
        }


        [Route("Error404")]
        public ActionResult Error404()
        {
            return View();
        }




        public ActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ReturnUrl.IsEmpty())
                {
                    if ((ReturnUrl.Contains("Admin") && User.IsInRole("Admin")) || ReturnUrl.Contains("Customer"))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return Redirect("https://localhost:44322/");
                }
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }



        [HttpPost]
        public ActionResult Login(string Username,string Password,bool? RememberMe,string ReturnUrl)
        {
            string Pass = HashPass.GetSha256(Password);
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == Username && t.Password == Pass);
            if(user == null)
            {
                return Content("error");
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), false, " " ,FormsAuthentication.FormsCookiePath);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
            if (RememberMe != null)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);
            if (ReturnUrl != null && ReturnUrl != "")
            {
                return Content(ReturnUrl);
            }
            return Content("https://localhost:44322/");
        }


        public ActionResult Logout(string ReturnUrl)
        {
            FormsAuthentication.SignOut();
            if (ReturnUrl.IsEmpty())
            {
                return RedirectToAction("Home");
            }
            return Redirect(ReturnUrl);
        }



        public ActionResult RegisterUser()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterUser(UserRegisterViewModel viewModel,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (db_BaziMarket.T_User.Any(t => t.Username == viewModel.Username))
                {
                    ModelState.AddModelError("Username", "این شماره موبایل قبلا ثبت شده است");
                    return Content("error");
                    //return RedirectToAction("RegisterUser", "Users");
                }
                string imageName = "user.png";
                if (Image != null)
                {
                    if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                    {
                        //ModelState.AddModelError("ProfileImage", "فرمت عکس اشتباه است !");
                        return Content("false");
                        return View(viewModel);
                        return RedirectToAction("RegisterUser", "Users");
                    }
                    if (Image.ContentLength > 500000)
                    {
                        //ModelState.AddModelError("ProfileImage","سایز عکس باید حداکثر 500 Kb باشد !");
                        return Content("false");
                        return View(viewModel);
                        return RedirectToAction("RegisterUser", "Users");
                    }
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Images/user-image/"), imageName));
                }
                User user = AutoMapperConfig.mapper.Map<UserRegisterViewModel, User>(viewModel);
                user.RegisterDate = DateTime.Now;
                user.ProfileImage = imageName;
                user.Password = HashPass.GetSha256(viewModel.Password);
                db_BaziMarket.T_User.Add(user);
                db_BaziMarket.SaveChanges();
                return Content(Url.Action("Login","Home"));
            }
            return Content("false");
            return View(viewModel);
        }





        public ActionResult NewProducts()
        {
            List<Product> products = db_BaziMarket.T_Product.OrderByDescending(t => t.RegisterDate).Take(5).ToList();
            return PartialView("NewProducts", products);
        }



    }
}