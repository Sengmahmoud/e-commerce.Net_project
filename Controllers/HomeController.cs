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
    //[System.Runtime.InteropServices.Guid("BE53E8A8-C658-4BD5-B0B5-2A598221E00F")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();

        }
       protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [AllowAnonymous]
       // [OutputCache(Duration =50,Location =System.Web.UI.OutputCacheLocation.Server,VaryByParam ="*") ]
        //to disable
        [OutputCache(Duration =0,VaryByParam ="*",NoStore =true)]
        public ActionResult Index()
        {
            var viewmodel = new ProductCartViewHome();

            if (Request.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.Where(p=>p.qnt>1).ToList();
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();




                viewmodel.Cart = cart;
                viewmodel.Products = products;
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;

            }
            else
            {
                var products = _context.Products.Where(p => p.qnt > 1).ToList();
                var Categories = _context.Categories.ToList();

                viewmodel.Products = products;
                viewmodel.Categoris = Categories;

            }
            return View(viewmodel);
            
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
       
            return View();
        }

        public ActionResult ProductDetails()
        {
           
            return View();
        }
        [Authorize]
        
        public ActionResult AdminPanel()
        {
            if (User.IsInRole("Admin"))
            {
                var product = _context.Products.Include(p => p.Category).OrderByDescending(p => p.id).ToList();
                var category = _context.Categories.ToList();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();


                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                var users = _context.Users.ToList();
                var orders = _context.Orders.Include(or=>or.User). ToList();

                var viewmodel = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = productscart,
                    Categoris = category,
                    Products = product,
                    users = users,
                    Orders=orders
                };
                return View(viewmodel);
            }
            else
                return HttpNotFound();
        }
        [Authorize]
        [Route("Home/Cart/{id}")]
        public ActionResult Cart(string id)
        {
            var userid = User.Identity.GetUserId();
            if (userid == id)
            {
                var cart = _context.Carts.Where(c => c.UserId == id).First();
                var products = _context.CartsProducts.Include(cp=>cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                // var product = _context.Carts.ToList();
                //var prod = _context.Products.ToList().Select(_context.CartsProducts);
                var category = _context.Categories.ToList();
                var viewmode = new ProductCartViewHome
                {
                    Cart = cart,
                   CatrtProduct = products,
                    Categoris=category
                };


                return View(viewmode);
            }
            else
                return HttpNotFound();
        }
       [Authorize]
        [Route("Home/AddtoCart")]
        [HttpPost]
        public ActionResult AddtoCart(int ProductId, int CartId)
        {
            var userid = User.Identity.GetUserId();
            var product = _context.Products.Single(c => c.id == ProductId);
             var cart = _context.Carts.SingleOrDefault(c => c.UserId == userid);

            var cartproduct = _context.CartsProducts.SingleOrDefault(
         c => c.CartId == cart.id
         && c.ProductId == product.id);
            if (cartproduct == null && product.qnt>1)
            {


                cartproduct = new CartProduct();
                cartproduct.ProductId = product.id;
                cartproduct.CartId = cart.id;
                cartproduct.qnt = 1;
                _context.CartsProducts.Add(cartproduct);
            }

            else if (product.qnt > cartproduct.qnt)
            {
                cartproduct.qnt++;
            }
            else
                return Content("Sorry Quntity no Valid ");
           
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
            //CartProduct cartprod = new CartProduct();
            //cartprod.CartId = CartId;
            //cartprod.ProductId = ProductId;
            //_context.CartsProducts.Add(cartprod);
            //_context.SaveChanges();
            //var userid = User.Identity.GetUserId();
            //var cart = _context.Carts.SingleOrDefault(c => c.UserId == userid);
            //cart.p
            //var product = _context.Products.SingleOrDefault(p => p.id == cartprod.ProductId);
            ////CartProduct car = new CartProduct();
            ////car.ID = 29;
            ////car.CartId = cart.id;
            ////car.ProductId = cartprod.ProductId;

            ////cartprod.CartId = cart.id;
            ////cartprod.ProductId = cartprod.ProductId;
            //cartprod.CartId = cart.id;
            //cartprod.ProductId = product.id;
            //_context.CartsProducts.Add(cartprod);
            ////_context.SaveChanges();
            //return RedirectToAction("Index", "Home");
            ////}
            ////else
            ////    return HttpNotFound();
            ////cartprod.CartId = cartprod1.id;
            ////cartprod.ProductId = cartprod.ProductId;

            //using (ManyToManyEntities conn = new ManyToManyEntities())
            //{

            //    /*
            //        * this steps follow to both entities
            //        * 
            //        * 1 - create instance of entity with relative primary key
            //        * 
            //        * 2 - add instance to context
            //        * 
            //        * 3 - attach instance to context
            //        */

            //    // 1
            //    Product p = new Product { ProductID = productID };
            //    // 2
            //    conn.Product.Add(p);
            //    // 3
            //    conn.Product.Attach(p);

            //    // 1
            //    Supplier s = new Supplier { SupplierID = supplierID };
            //    // 2
            //    conn.Supplier.Add(s);
            //    // 3
            //    conn.Supplier.Attach(s);

            //    // like previous method add instance to navigation property
            //    p.Supplier.Add(s);

            //    // call SaveChanges
            //    conn.SaveChanges();
            //}

        }
        [AllowAnonymous]
        [Route("Home/Search")]
        public ActionResult Search(string product_name)
        {
           // var category = _context.Categories.SingleOrDefault(c => c.Name == cart_name);
            
              var  product = _context.Products.SingleOrDefault(p => p.Name == product_name);


            if (product==null )
            
                return HttpNotFound();
            

            return RedirectToAction("Details/"+product.id,"Products");
        }
    }
    }
