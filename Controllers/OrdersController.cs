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
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        public OrdersController()
        {
            _context = new ApplicationDbContext();

        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Orders
        [Route("Orders/Index")]
        public ActionResult Index()
        {
           if (User.IsInRole("Admin"))
            {
                  var product = _context.Products.Include(p => p.Category).OrderByDescending(p => p.id).ToList();
                var category = _context.Categories.ToList();

                //
                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var orders = _context.Orders.Include(or => or.User).ToList();
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();


                //   
                var viewmodel = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = productscart,
                    Categoris = Categories,
                    Orders = orders,

                };
                return View(viewmodel);
            }
         else
                return HttpNotFound();
           
        }



        [Route("Orders/Show/{id}")]
        public ActionResult Show(int id)
        {
           if (User.IsInRole("Admin"))
           {
                var userid = User.Identity.GetUserId();

                var cart = _context.Carts.Where(c => c.UserId == userid).First();

                var order = _context.Orders.Single(or => or.id == id);
                var products = _context.OrderProducts.Include(cp => cp.Product).Where(cp => cp.OrderId == order.id).ToList();
                //  var product = _context.Carts.ToList();
                //var prod = _context.Products.ToList().Select(_context.CartsProducts);
                var cartproduct = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id);
                var category = _context.Categories.ToList();
                var viewmode = new ProductCartViewHome
                {
                    Cart = cart,
                    OrderProduct = products,
                    Categoris = category,
                    CatrtProduct = cartproduct

                };


                return View(viewmode);

            }
            return HttpNotFound();
        }

        [Route("Orders/Delete/{id}")]
       public ActionResult Delete(int id)
        {
            var orderindb = _context.Orders.SingleOrDefault(c => c.id == id);
            if (orderindb == null)
                return HttpNotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);
           
                else
                    _context.Orders.Remove(orderindb);
            
            _context.SaveChanges();
         
            return RedirectToAction("Index", "Orders");
        }


    }
}