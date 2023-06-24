using BaziMarket2.App_Start;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;
using BaziMarket2.OtherClases;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace BaziMarket2.Controllers
{
    public class ProductController : Controller
    {
        Db_BaziMarket db_BaziMarket = new Db_BaziMarket();
        public ActionResult Products(string Category,bool? FiltersOn)
        {
            if (Session["SubmitFilters"] != null)
            {
                ViewBag.Session = "session not null";
            }
            if (FiltersOn != null)
            {
                TempData["ProductsFilters"] = Session["SubmitFilters"];
                ViewBag.FiltersOn = "FiltersOn";
            }
            else
            {
                TempData["ProductsFilters"] = null;
                Session["SubmitFilters"] = null;
            }
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            if (!Category.IsEmpty())
            {
                ViewBag.Category = Category;
            }
            else
            {
                ViewBag.CategoryId = null;
            }
            return View();
        }



        public ActionResult P_Products(int? page, int? itemCount, int? sort, int? categoryId, int? sPrice, int? ePrice, bool? availableGoods, string category)
        {
            if (TempData["ProductsFilters"] != null)
            {
                var filters = TempData["ProductsFilters"] as SortedList<string, object>;
                page = filters["page"] as int?; itemCount = filters["itemCount"] as int?; sort = filters["sort"] as int?;
                categoryId = filters["categoryId"] as int?;
                sPrice = filters["sPrice"] as int?; ePrice = filters["ePrice"] as int?;
                availableGoods = filters["availableGoods"] as bool?;
            }


            List<Product> products = db_BaziMarket.T_Product.Where(t => t.IsActive).ToList();
            ViewData["Categories"] = db_BaziMarket.T_Category.ToList();
            ViewData["Sorts"] = Sort.GetSorts();
            ViewData["ItemCounts"] = new List<int>(){
                5,10,15,20,30
            };
            Category category1 = db_BaziMarket.T_Category.FirstOrDefault(t => t.Name == category);
            if (category1 != null)
            {
                categoryId = category1.Id;
                ViewBag.CategoryId = categoryId;
                ViewBag.Category = category1.Name;
                products = products.Where(t => t.CategoryId == categoryId).ToList();

            }
            else if (categoryId != null)
            {
                ViewBag.CategoryId = categoryId;
                products = products.Where(t => t.CategoryId == categoryId).ToList();
            }
            
            if (sPrice != null)
            {
                ViewBag.SPrice = sPrice;
                products = products.Where(t => t.Price >= sPrice.Value).ToList();
            }
            if (ePrice != null)
            {
                ViewBag.EPrice = ePrice;
                products = products.Where(t => t.Price <= ePrice.Value).ToList();
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
                if (sort == 5)
                {
                    products = products.OrderByDescending(t => t.Discount).ToList();
                }
            }
            ViewBag.AvailableGoods = false;
            if (availableGoods != null)
            {
                if (availableGoods.Value == true)
                {
                    ViewBag.AvailableGoods = true;
                    products = products.Where(t => t.InStockCount >= 1).ToList();
                }
            }
            if (itemCount == null)
            {
                itemCount = 5;
            }
            if (page == null)
            {
                page = 1;
            }
            int pageCount = products.Count / itemCount.Value;
            if (products.Count % itemCount != 0)
            {
                pageCount += 1;
            }
            ViewBag.CurrentPage = page;
            ViewBag.PageCount = pageCount;
            ViewBag.ItemCount = itemCount;
            products = products.Skip((page.Value - 1) * itemCount.Value).Take(itemCount.Value).ToList();
            return PartialView(AutoMapperConfig.mapper.Map<List<Product>, List<ProductsViewModel>>(products));
        }


        [HttpPost]
        public ActionResult ProductsFilters(int page, int itemCount, int? sort, int? categoryId, int? sPrice, int? ePrice, bool? availableGoods,string category)
        {
            //return Content($"page:{page}---itemCount:{itemCount}---sort:{sort}---catId:{categoryId}---SPrice:{sPrice}---EPrice:{ePrice}---ava:{availableGoods}------------");
            SortedList<string,object> pairs = new SortedList<string, object>()
            {
                {"sort",sort },
                {"itemCount",itemCount },
                {"sPrice",sPrice },
                {"ePrice",ePrice },
                {"categoryId",categoryId },
                {"availableGoods",availableGoods },
                {"page",page },
            };
            Session["SubmitFilters"] = pairs;
            return RedirectToAction("Products","Product",new {category=category,FiltersOn = true});
            //return Json(new {action = Url.Action("Products","Product"),partial = Url.Action("P_Products", "Product", new { page = page, itemCount = itemCount, sort = sort, categoryId = categoryId, sPrice = sPrice, ePrice = ePrice, availableGoods = availableGoods }) });
            //return Content(Url.Action("P_Products", "Product", new { page = page, itemCount = itemCount, sort = sort, categoryId = categoryId, sPrice = sPrice, ePrice = ePrice, availableGoods = availableGoods }));
            //return RedirectToAction("P_Products", "Product", new { page = page, itemCount = itemCount, sort = sort, categoryId = categoryId, sPrice = sPrice, ePrice = ePrice, availableGoods = availableGoods });
        }






        [Route("Product/Product/{EncodedName}")]
        public ActionResult Product(string EncodedName, int? tabNumber)
        {
            string name = HttpUtility.UrlDecode(EncodedName.Replace("-","+"), Encoding.UTF8);
            Product product = db_BaziMarket.T_Product.FirstOrDefault(t => t.Name == name);
            if (product != null)
            {
                if (product.IsActive)
                {
                    ViewBag.TabNumber = tabNumber;
                    return View(product);
                }
            }
            return Redirect("https://localhost:44322/Error404");
        }


        [HttpPost]
        public ActionResult AddToWishlist(int? productId)
        {
            Product product = db_BaziMarket.T_Product.Find(productId);
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (product != null && user != null)
            {
                if (!user.Products.Any(t => t.Id == productId))
                {
                    user.Products.Add(product);
                    db_BaziMarket.SaveChanges();
                    return Redirect($"https://localhost:44322/Product/Product/{product.EncodeName()}");
                }
            }
            return Content("false");
        }

        [HttpPost]
        public ActionResult RemoveFromWishlist(int? productId)
        {
            Product product = db_BaziMarket.T_Product.Find(productId);
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (product != null && user != null)
            {
                if (user.Products != null)
                {
                    user.Products.Remove(product);
                    db_BaziMarket.SaveChanges();
                    if (Request.UrlReferrer.AbsolutePath.Contains("Wishlist"))
                    {
                        return Redirect($"https://localhost:44322/Customer/Customer/Wishlist");
                    }
                    return Redirect($"https://localhost:44322/Product/Product/{product.EncodeName()}");
                }
            }
            return Content("false");
        }


        [HttpPost]
        public ActionResult AddToCart(int? productId, int productCount)
        {
            Product product = db_BaziMarket.T_Product.Find(productId);
            User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
            if (product != null && user != null)
            {
                if (user.Cart == null)
                {
                    user.Cart = new Cart()
                    {
                        Id = user.Id,
                    };
                }
                ProductCart productCart = new ProductCart()
                {
                    Number = productCount,
                    ProductId = productId,
                    CartId = user.Cart.Id
                };
                if (user.Cart.ProductCarts != null)
                {
                    if (user.Cart.ProductCarts.Any(t => t.ProductId == productId))
                    {
                        productCart = user.Cart.ProductCarts.FirstOrDefault(t => t.ProductId == productId);
                        productCart.Number += productCount;
                        if (productCart.Number > product.InStockCount)
                        {
                            productCart.Number = product.InStockCount;
                        }
                    }
                }
                db_BaziMarket.T_ProductCart.AddOrUpdate(productCart);
                db_BaziMarket.SaveChanges();
                if (Request.UrlReferrer.AbsolutePath.Contains("Wishlist"))
                {
                    return Redirect($"https://localhost:44322/Customer/Customer/NavBar");
                }
                return Redirect($"https://localhost:44322/Home/NavBar");
            };
            return Content("false");
        }


        [HttpPost]
        public ActionResult RemoveFromCart(int? productcartId, int productCount)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
                ProductCart productCart = db_BaziMarket.T_ProductCart.Find(productcartId);
                if (user != null && productCart != null)
                {
                    if (user.Cart != null)
                    {
                        if (user.Cart.ProductCarts != null)
                        {
                            if (productCart.Number > 1)
                            {
                                productCart.Number -= productCount;
                            }
                            else
                            {
                                user.Cart.ProductCarts.Remove(productCart);
                            }
                            db_BaziMarket.SaveChanges();
                            int? discountId = null;
                            discountId = TempData["discountId"] as int?;
                            //return RedirectToAction("UserCart","Customer/Customer");
                            return Redirect($"https://localhost:44322/Customer/Customer/Cart?discountId={discountId}");
                        }
                    }
                }
            }
            return Content("false");
        }









        [HttpPost]
        public ActionResult RegisterComment(int? productId, int rate, string commentText)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
                Product product = db_BaziMarket.T_Product.Find(productId);
                if (user != null && product != null)
                {
                    if (rate >= 1 && rate <= 5 && !commentText.IsEmpty())
                    {
                        db_BaziMarket.T_Comment.Add(new Comment()
                        {
                            IsActive = true,
                            Text = commentText,
                            RegisterDate = DateTime.Now,
                            Rate = rate,
                            ProductId = product.Id,
                            UserId = user.Id
                        });
                        db_BaziMarket.SaveChanges();
                        return Redirect($"https://localhost:44322/Product/Product/{product.EncodeName()}?tabNumber=3");
                    }
                }

            }
            return Content("false");
        }


        [HttpPost]
        public ActionResult RegisterQuestion(int? productId, string questionText)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = db_BaziMarket.T_User.FirstOrDefault(t => t.Username == User.Identity.Name);
                Product product = db_BaziMarket.T_Product.Find(productId);
                if (user != null && product != null)
                {
                    if (!questionText.IsEmpty())
                    {
                        db_BaziMarket.T_Question.Add(new Question()
                        {
                            IsActive = true,
                            Text = questionText,
                            RegisterDate = DateTime.Now,
                            ProductId = product.Id,
                            UserId = user.Id
                        });
                        db_BaziMarket.SaveChanges();
                        return Redirect($"https://localhost:44322/Product/Product/{product.EncodeName()}?tabNumber=4");
                    }
                }

            }
            return Content("false");
        }

    }
}