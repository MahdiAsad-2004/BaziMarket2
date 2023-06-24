using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaziMarket2.App_Start;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;

namespace BaziMarket2.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();


        public ActionResult AddCategory()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            //ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            return View(db_BaziMarket.T_Category.ToList());  
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryAddViewModel viewModel , HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            Category category = AutoMapperConfig.mapper.Map<CategoryAddViewModel, Category>(viewModel);
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
                string imageName = Guid.NewGuid().ToString().Replace("-","") + Path.GetExtension(Image.FileName);
                Image.SaveAs(Path.Combine(Server.MapPath("~/Images/category-image/"),imageName));
                category.Image = imageName;  
            }
            db_BaziMarket.T_Category.Add(category);
            db_BaziMarket.SaveChanges();
            return Json(new
            {
                message = $"دسته بندی ( {category.Name} ) با موفقیت افزوده شد",
                action = Url.Action("AddCategory","Category",null,Request.Url.Scheme)
            });
        }

     
    }
}