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
    public class CartsController : Controller
    {
        private ApplicationDbContext _context;
        public CartsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize]
        [Route("Carts/CheckOut/{cartid}")]
        public ActionResult CheckOut(int cartid)
        {


            var userid = User.Identity.GetUserId();
            var cart = _context.Carts.Where(c => c.id == cartid).First();


            var products = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
            //  var product = _context.Carts.ToList();
            //var prod = _context.Products.ToList().Select(_context.CartsProducts);
            var category = _context.Categories.ToList();
            var user = _context.Users.SingleOrDefault(u => u.Id == userid);
         
                var viewmode = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = products,
                    Categoris = category,
                    Users = user
                };

        
                return View(viewmode);
            
                //return HttpNotFound();
          //return View();
        }

        [Route("Carts/AddOrder")]
        public ActionResult AddOrder(string fname,string lname, string email,string phone,string address)
        {
             var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {

               
                /////user/////
                var user = _context.Users.SingleOrDefault(u => u.Id == userid);
                if (user.Id == null)
                {
                    //  user.UserName = user.UserName;
                    user.FristName = fname;
                    user.LastName = lname;
                    user.Email = email;
                    user.PhoneNumber = phone;
                    user.Address = address;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                else
                {
                    var userindb = _context.Users.SingleOrDefault(us => us.Id == user.Id);
                    //   userindb.UserName = user.UserName;
                    userindb.FristName = fname;
                    userindb.LastName = lname;
                    userindb.Email = user.Email;
                    userindb.PhoneNumber = phone;
                    userindb.Address = address;
                    _context.SaveChanges();
                }
                //////order/////////
                var order = new Order();
                order.userid = userid;
                order.Date = DateTime.Now;
                 _context.Orders.Add(order);
                _context.SaveChanges();
                /////orderproduct////////
                var cart = _context.Carts.SingleOrDefault(c => c.UserId == userid);
               var products = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                var productstbl = _context.Products.ToList();
              
                OrderProduct orderproduct = new OrderProduct();
                List<OrderProduct> orderslist = new List<OrderProduct>();
                //foreach (var item in products)
                //{
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var prods = _context.Products.Where(p => p.id == product.ProductId).ToList();
                        foreach (var item in prods)
                        {
                            item.qnt = item.qnt - product.qnt;
                        }
                        orderproduct.ProductId = product.ProductId;
                        orderproduct.OrderId = order.id;
                        orderproduct.qnt = product.qnt;
                        orderslist.Add(orderproduct);
                        orderproduct = new OrderProduct();


                    }


                }
                _context.OrderProducts.AddRange(orderslist);
                _context.CartsProducts.RemoveRange(products);
                _context.SaveChanges();
               
             return RedirectToAction("Index", "Home");
            }
            else
            {
                var cart = _context.Carts.SingleOrDefault(c => c.UserId == userid);
                var products = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                return RedirectToAction("CheckOut/" + cart.id, "Carts");
               }
            
        }

        [Route("Carts/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var productcartInDb = _context.CartsProducts.Where(c => c.ProductId == id).ToList();
          //  if (productcartInDb == null)
                //return HttpNotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            foreach (var product in productcartInDb)
            {
                if (product.qnt>1)
                {
                    product.qnt--;
                    
                }
                else
                    _context.CartsProducts.Remove(product);
            }
            _context.SaveChanges();
            var userid = User.Identity.GetUserId();
            return RedirectToAction("Cart/"+userid, "Home");
        }
    }
}