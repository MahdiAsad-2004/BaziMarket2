using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BaziMarket2.App_Start;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Web.Helpers;
using BaziMarket2.OtherClases;

namespace BaziMarket2.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();

        public ActionResult AllUsers(int? sort, int? page, string search)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            ViewData["Sorts"] = new List<Sort>()
            {
                new Sort(1,"تاریخ عضویت"),
                new Sort(2,"نقش"),
                new Sort(3,"نام")
            };
            List<User> users = db_BaziMarket.T_User.ToList();
            if (sort.HasValue)
            {
                if (sort.Value == 1)
                {
                    users = users.OrderByDescending(t => t.RegisterDate).ToList();
                }
                else if (sort.Value == 2)
                {
                    users = users.OrderBy(t => t.RoleId.Value).ToList();
                }
                else if (sort.Value == 3)
                {
                    users = users.OrderBy(t => t.FirstName).ThenBy(t => t.LastName).ToList();
                }
                ViewBag.Sort = sort;
            }
            if (!search.IsEmpty())
            {
                users = users.Where(t => string.Format(t.FirstName + t.LastName + t.Username).Contains(search)).ToList();
                ViewBag.Search = search;
            }
            if (!page.HasValue)
            {
                page = 1;
            }
            int x = users.Count / 6;
            if (users.Count % 6 == 0)
            {
                ViewBag.PageCount = x;
            }
            else
            {
                ViewBag.PageCount = x + 1;
            }
            ViewBag.CurrentPage = page;
            users = users.Skip((page.Value - 1) * 6).Take(6).ToList();
            List<UserListViewModel> userListViewModels = AutoMapperConfig.mapper.Map<List<User>, List<UserListViewModel>>(users);
            return View("AllUsers", userListViewModels);
        }


        public ActionResult AllUsersFilter(int? sort, int? page, string search)
        {
            return RedirectToAction("AllUsers", "Users", new { sort = sort, page = page, search = search });
        }



        public ActionResult RegisterUser()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            ViewData["UserRegisterList"] = AutoMapperConfig.mapper.Map<List<User>, List<UserRegisterListViewModel>>(db_BaziMarket.T_User.OrderByDescending(t => t.RegisterDate).Take(7).ToList());
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserRegisterViewModel viewModel, HttpPostedFileBase Image)
        {
            ViewData["UserRegisterList"] = AutoMapperConfig.mapper.Map<List<User>, List<UserRegisterListViewModel>>(db_BaziMarket.T_User.OrderByDescending(t => t.RegisterDate).Take(7).ToList());
            if (ModelState.IsValid)
            {
                if (db_BaziMarket.T_User.Any(t => t.Username == viewModel.Username))
                {
                    return Content("error");
                }
                string imageName = "user.png";
                if (Image != null)
                {
                    if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                    {
                        return Content("false");
                    }
                    if (Image.ContentLength > 500000)
                    {
                        return Content("false");   
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
                return Json(new { message = $"کاربر با نام کاربری  ({user.Username})  با موفقیت ثبت نام شد ", action = Url.Action("RegisterUser", "Users",null,Request.Url.Scheme) });
            }
            return Content("model not valid");
            return Content("false");
        }






        [HttpPost]
        public ActionResult ChangeUserActive(int UserId)
        {
            User user = db_BaziMarket.T_User.Find(UserId);
            if (user != null)
            {
                if (user.IsActive)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;
                }
                db_BaziMarket.SaveChanges();
            }
            return RedirectToAction("AllUsers");
        }


        [HttpPost]
        public ActionResult DeleteUser(string username)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == username);
            if (user != null)
            {
                if (user.RoleId != 1)
                {
                    if (user.Answers != null)
                    {
                        user.Answers.Clear();
                    }
                    db_BaziMarket.T_User.Remove(user);
                    db_BaziMarket.SaveChanges();
                    return RedirectToAction("AllUsers","Users",null);
                }
            }
            return Content("false");
        }


    }
}