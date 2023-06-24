using BaziMarket2.App_Start;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace BaziMarket2.Areas.Customer.Controllers
{
    public class CustomerController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();


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


        public ActionResult Panel()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (user == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            return View(AutoMapperConfig.mapper.Map<User,UserProfileViewModel>(user));
        }
        [HttpPost]
        public ActionResult EditUserPanel(UserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == viewModel.Username);
            if (user == null)
            {
                return Content("false");
            }
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Address = viewModel.Address;
            db_BaziMarket.T_User.AddOrUpdate(user);
            db_BaziMarket.SaveChanges();
            return Redirect(Url.Action("Panel", "Customer",null,Request.Url.Scheme));
            return RedirectToAction("Panel","Customer");
            //return Content($"Username:{user.Username}---Id:{user.Id}---Pass:{user.Password}---");
        }

        public ActionResult ChageUserProfileImage(string Username, HttpPostedFileBase Image)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == Username);
            if (user != null)
            {
                if (Image != null)
                {
                    if (Image.ContentLength > 500000)
                    {
                        return Content("false");
                    }
                    if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                    {
                        return Content("false");
                    }
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Images/user-image/"), imageName));
                    if (!user.ProfileImage.IsEmpty() && user.ProfileImage != "user.png")
                    {
                        FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Images/user-image/"), user.ProfileImage));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    user.ProfileImage = imageName;
                    db_BaziMarket.SaveChanges();
                    return RedirectToAction("Panel", "Customer");
                }
                return Content($"user null----Username:{Username}---");
            
            }
            return Content("Image null");    
        }


        public ActionResult ChangeUserPassword()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (user == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            return View(AutoMapperConfig.mapper.Map<User,UserChangePasswordViewModel>(user));
        }

        [HttpPost]
        public ActionResult ChangeUserPassword(UserChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == viewModel.Username);
                if (user != null)
                {
                    if (user.Password == HashPass.GetSha256(viewModel.Password))
                    {
                        user.Password = HashPass.GetSha256(viewModel.NewPassword);
                        db_BaziMarket.SaveChanges();
                        return RedirectToAction("ChangeUserPassword", "Customer");
                    }
                    else
                    {
                        return Json(new {error = "رمز عبور صحیح نیست"});
                    }
                }
            }
            return Content("false");
        }






        [HttpPost]
        public ActionResult DeleteUser(string username)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == username);
            if (user != null)
            {
                db_BaziMarket.T_User.Remove(user);
                db_BaziMarket.SaveChanges();
                //FormsAuthentication.SignOut();
                return Content("treu");
            }
            return Content("false");
        }






        public ActionResult Cart(int? discountId)
        {
            Cart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
                if (user != null)
                {
                    if (user.Cart != null)
                    {
                        if (user.Cart.ProductCarts != null)
                        {
                            if (user.Cart.ProductCarts.Count > 0)
                            {
                                if (discountId != null)
                                {
                                    ViewData["Discount"] = db_BaziMarket.T_Discount.Find(discountId);
                                }
                                cart = user.Cart;
                            }

                        }
                    }
                }
            }
            return View(cart);
        }

        


        public ActionResult CheckDiscountCode(string discountCode)
        {
            Discount discount = db_BaziMarket.T_Discount.FirstOrDefault(t => t.Code == discountCode);
            if (discount == null)
            {
                return RedirectToAction("Cart","Customer");
            }
            return Json(new { message = "ok", action = Url.Action("Cart", "Customer", new { discountId = discount.Id }, Request.Url.Scheme) });
            return Content("ok");
        
        }





        public ActionResult Wishlist()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (user == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<Product> products = null;
            if (user.Products != null)
            {
                if (user.Products.Count > 0)
                {
                    products = user.Products;
                }
            }
            return View(products);
        }


    }
}