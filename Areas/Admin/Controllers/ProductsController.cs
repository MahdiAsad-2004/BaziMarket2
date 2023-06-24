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
using System.Web.WebPages;

namespace BaziMarket2.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();



        public ActionResult P_AllProducts(string search, int? sort, int page, int itemCount)
        {
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            List<ProductAllViewModel> products = AutoMapperConfig.mapper.Map<List<Product>, List<ProductAllViewModel>>(db_BaziMarket.T_Product.ToList());
            if (!search.IsEmpty())
            {
                products = products.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
                ViewBag.Search = search;
            }
            if (sort != null)
            {
                ViewBag.Sort = sort;
                if (sort == 1)
                {
                    products = products.OrderBy(t => t.Price).ToList();
                }
                if (sort == 2)
                {
                    products = products.OrderByDescending(t => t.Price).ToList();
                }
                if (sort == 3)
                {
                    products = products.OrderByDescending(t => t.Sold).ToList();
                }
                if (sort == 4)
                {
                    products = products.OrderByDescending(t => t.RegisterDate).ToList();
                }
            }
            ViewBag.ItemCount = itemCount;
            int x = products.Count / itemCount;
            if (products.Count % itemCount == 0)
            {
                ViewBag.PageCount = x;
            }
            else
            {
                ViewBag.PageCount = x + 1;
            }
            ViewBag.CurrentPage = page;
            products = products.Skip((page - 1) * itemCount).Take(itemCount).ToList();
            return PartialView(products);
        }

        public ActionResult AllProducts()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            return View();
        }

        //[HttpPost]
        //public ActionResult AllProducts(ProductAllViewModel viewModel, HttpPostedFileBase Image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (db_BaziMarket.T_Product.Find(viewModel.Id).Name != viewModel.Name.Trim())
        //        {
        //            if (db_BaziMarket.T_Product.Any(t => t.Name == viewModel.Name.Trim()))
        //            {
        //                return Json(new { error = $"تغیرات انجام نشد!\n\nمحصولی با نام ( {viewModel.Name} ) وجود دارد" });
        //            }
        //        }
        //        string imageName = "product.png";
        //        if (Image == null)
        //        {
        //            if (Image.ContentLength > 500000)
        //            {
        //                return Json(new { error = "تغیرات انجام نشد!\n\nحجم عکس نمیتواند بیش از 500 Kb باشد" });
        //            }
        //            if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
        //            {
        //                return Json(new { error = "تغیرات انجام نشد!\n\nفرمت عکس اشتباه است" });
        //            }
        //            imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
        //            Image.SaveAs(Path.Combine(Server.MapPath("~/Images/product-image"), imageName));
        //        }
        //        Product product = AutoMapperConfig.mapper.Map<ProductAllViewModel, Product>(viewModel);
        //        product.Image = imageName;
        //        db_BaziMarket.T_Product.AddOrUpdate(product);
        //        db_BaziMarket.SaveChanges();
        //        return Json(new { message = "تغییرات با موفقیت انجام شد ", action = RedirectToAction("AllProducts") });


        //        return RedirectToAction("AllProducts");
        //        return Content(Url.Action("AllProducts"));
        //        return Content($"Is Vald__________name:{viewModel.Name}--price:{viewModel.Price}--count:{viewModel.InStockCount}--sold:{viewModel.Sold}--isactive:{viewModel.IsActive}--registerdate:{viewModel.RegisterDate}--discount:{viewModel.Discount}--catId:{viewModel.CategoryId}");
        //        return RedirectToAction("P_AllProducts", new { search = "", sort = "", page = 1 });
        //    }
        //    return Content("false");
        //}


        public ActionResult AllProductsFilter(string search, int? sort, int page, int itemCount)
        {
            //return Content($"Search:{search}----sort:{sort}-----page:{page}");
            TempData["Filters"] = new SortedList<string, object>()
            {
                {"sort",ViewBag.Sort },
                {"itemCount",ViewBag.ItemCount },
                {"sPrice",ViewBag.SPrice },
                {"ePrice",ViewBag.Eprice },
                {"categoryId",ViewBag.CategoryId },
                {"availableGoods",ViewBag.AvailableGoods },
                {"page",ViewBag.CurrentPage },
            };
            return RedirectToAction("P_AllProducts", new { search = search, sort = sort, page = page, itemCount = itemCount });
        }













        public ActionResult AddProduct()
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            ViewData["Products"] = AutoMapperConfig.mapper.Map<List<Product>, List<ProductAddListViewModel>>(db_BaziMarket.T_Product.OrderByDescending(t => t.RegisterDate).Take(6).ToList());
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAddViewModel viewModel, HttpPostedFileBase Image)
        {
            ViewData["Products"] = AutoMapperConfig.mapper.Map<List<Product>, List<ProductAddListViewModel>>(db_BaziMarket.T_Product.OrderByDescending(t => t.RegisterDate).Take(6).ToList());
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            string imageName = "product.png";
            if (ModelState.IsValid)
            {
                if (db_BaziMarket.T_Product.Any(t => t.Name == viewModel.Name))
                {
                    ModelState.AddModelError("Name", "محصول با این نام وجود دارد!");
                    return View(viewModel);
                }
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
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Images/product-image"), imageName));
                }
                Product product = AutoMapperConfig.mapper.Map<ProductAddViewModel, Product>(viewModel);
                product.Image = imageName;
                product.RegisterDate = DateTime.Now;
                product.Sold = 0;
                db_BaziMarket.T_Product.Add(product);
                db_BaziMarket.SaveChanges();
                return Json(new { message = string.Format($"محصول ( {product.Name} ) با موفقیت افزوده شد."), action = Url.Action("AddProduct", "Products", null, Request.Url.Scheme) });
            }
            return Content("false");
        }







        public ActionResult EditProduct(int? ProductId)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<Category> categories = db_BaziMarket.T_Category.ToList(); ;
            if (product.CategoryId != null)
            {
                categories.Remove(product.Category);
            }
            ViewData["Categories"] = categories;
            return View(AutoMapperConfig.mapper.Map<Product, ProductAllViewModel>(product));
        }
        [HttpPost]
        public ActionResult EditProduct(ProductAllViewModel viewModel, HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return Content("not valid");
            }
            Product product = AutoMapperConfig.mapper.Map<ProductAllViewModel, Product>(viewModel);
            if (ImageFile != null)
            {
                if (ImageFile.ContentLength > 500000)
                {
                    return Content("false");
                }
                if (ImageFile.ContentType != "image/jpeg" && ImageFile.ContentType != "image/png")
                {
                    return Content("false");
                }
                string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ImageFile.FileName);
                ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Images/product-image/"), imageName));
                product.Image = imageName;
            }
            //return Content($"Image:{viewModel.Image}----");
            db_BaziMarket.T_Product.AddOrUpdate(product);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("EditProduct", "Products", new { ProductId = product.Id });
            //return Content($"Name:{viewModel.Name}---Id:{viewModel.Id}---Price:{viewModel.Price}---Count:{viewModel.InStockCount}---Sold:{viewModel.Sold}---Active:{viewModel.IsActive}---Date:{viewModel.RegisterDate}---Dis:{viewModel.Discount}---CatId:{viewModel.CategoryId}");
        }
        [HttpPost]
        public ActionResult DeleteProduct(int? productId)
        {
            //return Content("ruuun");
            Product product = db_BaziMarket.T_Product.Find(productId);
            if (product == null)
            {
                return Content("false");
            }
            string productName = product.Name;
            if (!product.Image.IsEmpty() && product.Image != "product.png")
            {
                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Images/product-image/"), product.Image));
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            if (product.Pictures != null)
            {
                foreach (var item in product.Pictures)
                {
                    if (!item.Name.IsEmpty())
                    {
                        FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Images/product-pictures/"), item.Name));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
            }
            db_BaziMarket.T_Product.Remove(product);
            try
            {
                db_BaziMarket.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(ex.Message + "----------" + ex.InnerException.Message + "---------" + ex.InnerException.InnerException.Message + "------" + ex.Source + "********" + ex.InnerException.Source);
            }
            //return Content("deleted");
            return Json(new
            {
                message = $"محصول ( {productName} ) با موفقیت حذف شد ",
                action = Url.Action("AllProducts", "Products", null, Request.Url.Scheme)
            });
            //return RedirectToAction("AllProducts","Products");
        }






        public ActionResult ProductContent(int? ProductId)
        {
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            ViewBag.AdminName = user.FirstName + " " + user.LastName;
            ViewBag.AdminImage = user.ProfileImage;
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            ViewBag.ProductId = ProductId;
            return View();
        }




        public ActionResult P_ViewProduct(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null || ProductId == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            return PartialView(product);
        }






        public ActionResult AddDescription(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<int> ints = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            if (product.Descriptions != null)
            {
                foreach (var item in product.Descriptions)
                {
                    ints.Remove(item.Index);
                }
            }
            ViewBag.ProductId = ProductId;
            ViewData["Indexes"] = ints;
            ViewData["Descriptions"] = product.Descriptions;
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddDescription(DescriptionAddViewModel viewModel, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Index == null)
                {
                    List<int> ints = new List<int>()
                {
                    1,2,3,4,5,6,7,8,9,10
                };
                    foreach (var item in db_BaziMarket.T_Product.Find(viewModel.ProductId).Descriptions)
                    {
                        ints.Remove(item.Index);
                    }
                    viewModel.Index = ints.Min();
                }
                string imageName = null;
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
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath("~/Images/description-image"), imageName));
                }
                Description description = AutoMapperConfig.mapper.Map<DescriptionAddViewModel, Description>(viewModel);
                description.Image = imageName;
                db_BaziMarket.T_Description.Add(description);
                db_BaziMarket.SaveChanges();
                return Json(new
                {
                    message = $"توضیح ( {description.Title} ) با موفقیت افزوده شد",
                    partial = Url.Action("AddDescription", "Products", new { ProductId = description.ProductId }, Request.Url.Scheme)
                });
            }
            return Content("false");
        }


        [HttpPost]
        public ActionResult DeleteDescription(int? descriptionId)
        {
            Description description = db_BaziMarket.T_Description.Find(descriptionId);
            if (description == null)
            {
                return Content("false");
            }
            if (!description.Image.IsEmpty())
            {
                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Images/description-image/"), description.Image));
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            int? productId = description.ProductId;
            if (productId == null)
            {
                return Content("false");
            }
            db_BaziMarket.T_Description.Remove(description);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("AddDescription", "Products", new { ProductId = productId });
        }






        public ActionResult P_AddPicture(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<int> ints = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            if (product.Pictures != null)
            {
                foreach (var item in product.Pictures)
                {
                    ints.Remove(item.Index);
                }
            }
            ViewBag.ProductId = ProductId;
            ViewData["Indexes"] = ints;
            ViewData["Pictures"] = product.Pictures;
            return PartialView();
        }

        [HttpPost]
        public ActionResult P_AddPicture(PictureAddViewModel viewModel, HttpPostedFileBase Image)
        {
            if (Image == null)
            {
                return Content("false");
            }
            if (Image.ContentLength > 500000)
            {
                return Content("false");
            }
            if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
            {
                return Content("false");
            }
            string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Image.FileName);
            Image.SaveAs(Path.Combine(Server.MapPath("~/Images/product-pictures/"), imageName));
            Picture picture = AutoMapperConfig.mapper.Map<PictureAddViewModel, Picture>(viewModel);
            picture.Name = imageName;
            //return Content($"Name:{picture.Name}---ProductId:{picture.ProductId}---Index:{picture.Index}---Id:{picture.Id}");
            //db_BaziMarket.T_Picture.Add(new Picture()
            //{
            //    Name = "ssdcdscscs",
            //    Index = 22,
            //    ProductId = null
            //});
            //return Content($"Id:{picture.Id}----Name:{picture.Name}---Index:{picture.Index}----ProId:{picture.ProductId}------");
            db_BaziMarket.T_Picture.Add(picture);
            db_BaziMarket.SaveChanges();
            //return Content("picture saved");
            return RedirectToAction("P_AddPicture", "Products", new { ProductId = picture.ProductId });
        }
        [HttpPost]
        public ActionResult DeletePicture(int? pictureId)
        {
            Picture picture = db_BaziMarket.T_Picture.Find(pictureId);
            if (picture != null)
            {
                if (!picture.Name.IsEmpty())
                {
                    FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/Images/product-pictures/"), picture.Name));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    int? productId = picture.ProductId;
                    if (productId == null)
                    {
                        return Content("false");
                    }
                    db_BaziMarket.T_Picture.Remove(picture);
                    db_BaziMarket.SaveChanges();
                    return RedirectToAction("P_AddPicture", "Products", new { ProductId = productId });
                }

            }
            return Content("false");
        }






        public ActionResult P_AddSpecification(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<int> ints = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
            };
            if (product.Specifications != null)
            {
                foreach (var item in product.Specifications)
                {
                    ints.Remove(item.Index);
                }
            }
            ViewBag.ProductId = ProductId;
            ViewData["Indexes"] = ints;
            ViewData["Specifications"] = product.Specifications;
            return PartialView();
        }

        [HttpPost]
        public ActionResult P_AddSpecification(SpecificationAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Specification specification = AutoMapperConfig.mapper.Map<SpecificationAddViewModel, Specification>(viewModel);
                db_BaziMarket.T_Specification.Add(specification);
                db_BaziMarket.SaveChanges();
                return Json(new
                {
                    message = $"مشخصه ({specification.Title}) با موفقیت افزوده شد",
                    partial = Url.Action("P_AddSpecification", "Products", new { ProductId = specification.ProductId }, Request.Url.Scheme)
                });
            }
            return Content("false");
        }
        [HttpPost]
        public ActionResult DeleteSpecification(int? specificationId)
        {
            Specification specification = db_BaziMarket.T_Specification.Find(specificationId);
            if (specification == null)
            {
                return Content("false");
            }
            int? productId = specification.ProductId;
            db_BaziMarket.T_Specification.Remove(specification);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("P_AddSpecification", "Products", new { ProductId = productId });
        }








        public ActionResult P_AddProperty(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<int> ints = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            if (product.Properties != null)
            {
                foreach (var item in product.Properties)
                {
                    ints.Remove(item.Index);
                }
            }
            ViewBag.ProductId = ProductId;
            ViewData["Indexes"] = ints;
            ViewData["Properties"] = product.Properties;
            return PartialView();
        }
        [HttpPost]
        public ActionResult P_AddProperty(PropertyAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Property property = AutoMapperConfig.mapper.Map<PropertyAddViewModel, Property>(viewModel);
                db_BaziMarket.T_Property.Add(property);
                db_BaziMarket.SaveChanges();
                return Json(new
                {
                    message = $"ویژگی ({property.Text}) با موفقیت افزوده شد",
                    partial = Url.Action("P_AddProperty", "Products", new { ProductId = property.ProductId }, Request.Url.Scheme)
                });
            }
            return Content("false");
          
        }
        [HttpPost]
        public ActionResult DeleteProperty(int? propertyId)
        {
            Property property = db_BaziMarket.T_Property.Find(propertyId);
            if (property == null)
            {
                return Content("false");
            }
            int? productId = property.ProductId;
            db_BaziMarket.T_Property.Remove(property);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("P_AddProperty", "Products", new { ProductId = productId });
        }






        public ActionResult P_EditComments(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<CommentEditViewModel> commentEditViewModel = AutoMapperConfig.mapper.Map<List<Comment>, List<CommentEditViewModel>>(product.Comments.OrderByDescending(t => t.RegisterDate).ToList());
            return PartialView(commentEditViewModel);
        }
        [HttpPost]
        public ActionResult ChangeCommentIsActive(int? commentId)
        {
            Comment comment = db_BaziMarket.T_Comment.Find(commentId);
            if (comment != null)
            {
                comment.IsActive = !comment.IsActive;
                db_BaziMarket.SaveChanges();
                return RedirectToAction("P_EditComments", "Products", new { ProductId = comment.ProductId });
            }
            return Content("false");

        }
        [HttpPost]
        public ActionResult DeleteComment(int? commentId)
        {
            Comment comment = db_BaziMarket.T_Comment.Find(commentId);
            if (comment != null)
            {
                int productId = comment.ProductId;
                db_BaziMarket.T_Comment.Remove(comment);
                db_BaziMarket.SaveChanges();
                return RedirectToAction("P_EditComments", "Products", new { ProductId = productId });
            }
            return Content("false");

        }






        public ActionResult P_EditQuestions(int? ProductId)
        {
            Product product = db_BaziMarket.T_Product.Find(ProductId);
            if (product == null)
            {
                return Redirect("https://localhost:44322/Error404");
            }
            List<QuestionEditViewModel> QuestionEditViewModel = AutoMapperConfig.mapper.Map<List<Question>, List<QuestionEditViewModel>>(product.Questions.OrderByDescending(t => t.RegisterDate).ToList());
            return PartialView(QuestionEditViewModel);
        }
        [HttpPost]
        public ActionResult ChangeQuestionIsActive(int? questionId)
        {
            Question question = db_BaziMarket.T_Question.Find(questionId);
            if (question == null)
            {
                return Content("false");
            }
            question.IsActive = !question.IsActive;
            db_BaziMarket.SaveChanges();
            return RedirectToAction("P_EditQuestions", "Products", new { ProductId = question.ProductId });
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int? questionId)
        {
            Question question = db_BaziMarket.T_Question.Find(questionId);
            if (question != null)
            {
                int productId = question.ProductId;
                db_BaziMarket.T_Question.Remove(question);
                db_BaziMarket.SaveChanges();
                return RedirectToAction("P_EditQuestions", "Products", new { ProductId = productId });
            }
            return Content("false");

        }

        [HttpPost]
        public ActionResult ChangeAnswerIsActive(int? questionId)
        {
            Question question = db_BaziMarket.T_Question.Find(questionId);
            if (question == null)
            {
                if (question.Answer != null)
                {
                    return Content("false");
                }
            }
            question.Answer.IsActive = !question.Answer.IsActive;
            db_BaziMarket.SaveChanges();
            return RedirectToAction("P_EditQuestions", "Products", new { ProductId = question.ProductId });
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int? questionId)
        {
            Question question = db_BaziMarket.T_Question.Find(questionId);
            if (question == null)
            {
                if (question.Answer != null)
                {
                    return Content("false");
                }
            }
            db_BaziMarket.T_Answer.Remove(question.Answer);
            db_BaziMarket.SaveChanges();
            return RedirectToAction("P_EditQuestions", "Products", new { ProductId = question.ProductId });
        }











    }


}