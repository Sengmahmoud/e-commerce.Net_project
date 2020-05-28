using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using e_commerce.ViewModel;
using Microsoft.AspNet.Identity;

namespace e_commerce.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Categories
        [Route("Categories/Index")]
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
            }
            else
            {
                var products = _context.Products.ToList();
                var Categories = _context.Categories.ToList();
                viewmodel.Products = products;

                viewmodel.Categoris = Categories;
            }

            //var categories = _context.Categories.ToList();
            return View(viewmodel);
        }
        [Authorize]
        public ActionResult New()
        {
            if (User.IsInRole("Admin"))
            {
              var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                 var products = _context.Products.Include(p => p.Category).ToList();

                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();


                viewmodel.Cart = cart;
               viewmodel.Category = new Category();
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;

                return View("CategoryForm", viewmodel);
            }
            else
               return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                 var products = _context.Products.Include(p => p.Category).ToList();

                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                viewmodel.Category = category;

                viewmodel.Cart = cart;
             //    viewmodel.Product = new Product();
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;

                return View("CategoryForm", viewmodel);
            }

                if (category.id == 0)
                _context.Categories.Add(category);
            else
            {
                var categoryinDB = _context.Categories.Single(c => c.id == category.id);
                categoryinDB.Name = category.Name;
                
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Categories");
         
        }


        public ActionResult Edit(int id)
        {/////
            var viewmodel = new ProductCartViewHome();


            var userid = User.Identity.GetUserId();
            var cart = _context.Carts.Where(c => c.UserId == userid).First();
          

            var Categories = _context.Categories.ToList();
            var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();

            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            viewmodel.Cart = cart;
            var category = _context.Categories.SingleOrDefault(c => c.id == id);
            viewmodel.Categoris = Categories;
            viewmodel.CatrtProduct = productscart;
            viewmodel.Products = products;
            viewmodel.Category = category;

            if (category == null)
                return HttpNotFound();

            ///////



            return View("CategoryForm", viewmodel);
          //  return View();
        }
        [Route("Categories/show/{id}")]
        public ViewResult Show(int id)
        {
            //
            var viewmodel = new ProductCartViewHome();

            if (Request.IsAuthenticated)
            {


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();


                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();

                var products = _context.Products.Where(p => p.CategoryId == id).ToList();
                viewmodel.Cart = cart;
                viewmodel.Cart = cart;
                viewmodel.Products = products;
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;
            }



            //


            else
            {
                var Categories = _context.Categories.ToList();
                var products = _context.Products.Where(p => p.CategoryId == id).ToList();
                viewmodel.Categoris = Categories;

                viewmodel.Products = products;
            }



            return View(viewmodel);
        }
        [Authorize(Roles = "Admin")]
       [Route("Categories/Delete/{id}")]
     //   [HttpPost]
        public ActionResult Delete(int id)
        {
            var categoryInBD = _context.Categories.Single(c => c.id == id);
            if (categoryInBD == null)
                return HttpNotFound();
            _context.Categories.Remove(categoryInBD);
            _context.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
    }
}