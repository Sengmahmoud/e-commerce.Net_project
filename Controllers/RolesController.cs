using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using e_commerce.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_commerce.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;
        public RolesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Roles
        [Route("Roles/Index")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.ToList();
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();

                var Roles = _context.Roles.ToList();




                //   
                var viewmodel = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = productscart,
                    Categoris = Categories,
                    Products = products,
                    Roles = Roles
                };
                return View(viewmodel);
            }
            else
                return HttpNotFound();

        }

        public ActionResult New()
        {
            if (User.IsInRole("Admin"))
            {
                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.ToList();
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();

                var Roles = _context.Roles.ToList();




                //   
                var viewmodel = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = productscart,
                    Categoris = Categories,
                    Products = products,
                    //Role=new IdentityRole()
                };
                return View("RolesForm", viewmodel);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Save(string Role)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Role;
            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
          
        }

    }
}