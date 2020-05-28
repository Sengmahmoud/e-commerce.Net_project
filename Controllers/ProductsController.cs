using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_commerce.ViewModel;
using System.IO;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace e_commerce.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        public ProductsController()
        {
            _context = new ApplicationDbContext();   
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Products
        [Route("Products/Index")]
        [AllowAnonymous]
        public ViewResult Index()
        {

            var viewmodel = new ProductCartViewHome();

            if (Request.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.Include(p => p.Category).ToList();
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                viewmodel.Cart = cart;
                viewmodel.Products = products;
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;



                return View(viewmodel);

            }


            else
            {

                var products = _context.Products.Include(p => p.Category).ToList();
                var Categories = _context.Categories.ToList();
                viewmodel.Products = products;
                viewmodel.Categoris = Categories;
                return View(viewmodel);
            }
        }
        [Authorize]
         public ActionResult New()
        {
            if (User.IsInRole("Admin"))
            {
                ///////////////
                var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.Include(p => p.Category).ToList();

                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();


                viewmodel.Cart = cart;
                viewmodel.Product = new Product();
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;




                ///////
                var categories = _context.Categories.ToList();
                var viewmodel1 = new ProductCategoryModelForm
                {

                    //to  give id=0 value 
                    Product = new Product(),
                    Categories = categories
                };
                return View("ProductForm", viewmodel);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Save(Product product,HttpPostedFileBase file)
        {
            //very important note//The name of the HttpPostedFileBase parameter and the name of HTML FileUpload element must be exact same, 
            //otherwise the HttpPostedFileBase parameter will be NULL

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = Path.GetFileName(file.FileName);
                    string path = Path.Combine(
                                           Server.MapPath("~/resources/"), pic);
                    // file is uploaded
                    file.SaveAs(path);


                    //file.SaveAs(HttpContext.Server.MapPath("~/resources/")
                    //                                      + file.FileName);
                    product.img = file.FileName;

                }
                if (product.id == 0)
                {

                    _context.Products.Add(product);
                }

                else
                {

                    var productInDB = _context.Products.Single(c => c.id == product.id);

                    //file.SaveAs(HttpContext.Server.MapPath("~/resources/")
                    //                                     + file.FileName);
                    //string pic = System.IO.Path.GetFileName(file.FileName);
                    productInDB.img = file.FileName;
                    productInDB.Name = product.Name;
                    productInDB.price = product.price;
                    productInDB.qnt = product.qnt;
                    productInDB.CategoryId = product.CategoryId;
                    //    TryUpdateModel(productInDB);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.Include(p => p.Category).ToList();

                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();


                viewmodel.Cart = cart;
                viewmodel.Product = new Product();
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;

                return View("ProductForm",viewmodel);
            }
        }



        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Admin"))
            {
                ///////////////
                var viewmodel = new ProductCartViewHome();

               
                    var userid = User.Identity.GetUserId();
                    var cart = _context.Carts.Where(c => c.UserId == userid).First();
                    var products = _context.Products.Include(p => p.Category).ToList();
                    var product = _context.Products.SingleOrDefault(c => c.id == id);
                    var Categories = _context.Categories.ToList();
                    var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();


                    viewmodel.Cart = cart;
                    viewmodel.Product = product;
                    viewmodel.Categoris = Categories;
                    viewmodel.CatrtProduct = productscart;


               


                ///
               // var product = _context.Products.SingleOrDefault(c => c.id == id);
                if (product == null)
                    return HttpNotFound();
                //var viewmodel = new ProductCategoryModelForm
                //{
                //    Product = product,
                //    Categories = _context.Categories.ToList()
                //};
                return View("ProductForm", viewmodel);
            }
            else
                return HttpNotFound();
        }

        [AllowAnonymous]
        [Route("Products/Details/{id}")]
        public ActionResult Details(int id)
        {

                var viewmodel = new ProductCartViewHome();

                if (Request.IsAuthenticated)
                {
                    var userid = User.Identity.GetUserId();
                    var cart = _context.Carts.Where(c => c.UserId == userid).First();
                    var products = _context.Products.Include(c => c.Category).Where(p => p.id == id);
                    var Categories = _context.Categories.ToList();
                    var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();




                    viewmodel.Cart = cart;
                    viewmodel.Products = products;
                    viewmodel.Categoris = Categories;
                    viewmodel.CatrtProduct = productscart;

                }
                else
                {
                var products = _context.Products.Include(c => c.Category).Where(p => p.id == id);
                var Categories = _context.Categories.ToList();

                    viewmodel.Products = products;
                    viewmodel.Categoris = Categories;

                }




                ///
                return View(viewmodel);
        }

        //DELETE/Product/1
        [Authorize(Roles = "Admin")]
       [HttpPost]
        public ActionResult Delete(int id)
        {
            var productInDb = _context.Products.Single(c => c.id == id);
            if (productInDb == null)
                return HttpNotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Products.Remove(productInDb);
            _context.SaveChanges();

             return RedirectToAction("Index", "Products");
        }
    }
}