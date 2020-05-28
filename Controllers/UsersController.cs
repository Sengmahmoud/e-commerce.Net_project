using e_commerce.Models;
using e_commerce.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace e_commerce.Controllers
{
   [Authorize]
    
    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        //// private Task UserManager;
        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Users
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

                var users = _context.Users.ToList();



                //   
                var viewmodel = new ProductCartViewHome
                {
                    Cart = cart,
                    CatrtProduct = productscart,
                    Categoris = Categories,
                    Products = products,
                    Roles = Roles,
                    users = users,

                };
                return View(viewmodel);
            }
            else return HttpNotFound();

        }
        [Route("Users/Edit/{id}")]
        public ActionResult Edit(string id)
        {
            if (User.IsInRole("Admin"))
            {
                /////////////////
                var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
               // var user = _context.Users.SingleOrDefault(c => c.Id ==userid);
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var products = _context.Products.Include(p => p.Category).ToList();
               // var product = _context.Products.SingleOrDefault(c => c.id == id);
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                var user = _context.Users.Single(u => u.Id == id);
                var Roles = _context.Roles.ToList();
                viewmodel.Cart = cart;
                // viewmodel.Product = product;
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;
                viewmodel.Users = user;
                viewmodel.Roles = Roles;


                // viewmodel.users = usersin;




                ///
              //  var product = _context.Products.SingleOrDefault(c => c.id == id);
                if (user == null)
                    return HttpNotFound();
                //var viewmodel = new ProductCategoryModelForm
                //{
                //    Product = product,
                //    Categories = _context.Categories.ToList()
                //};
                return View("UserForm", viewmodel);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Save(string Id, string Roles)
        {
            var viewmodel = new ProductCartViewHome();
            var users = _context.Users.ToList();
         //   var Roles = _context.Roles.ToList();
            viewmodel.users = users;
        //    viewmodel.Roles = Roles;
            var role = _context.Roles.Where(r => r.Name == Roles);
            var user = _context.Users.Where(u => u.Id == Id);
           // var users = _context.Users.ToList();

            var roles = _context.Roles.ToList();
            if (user != null)
            {
                await UserManager.AddToRoleAsync(Id,Roles);
                return RedirectToAction("Index");
            }
            else
                return HttpNotFound();
            

          //  ApplicationUser user = _context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
           
            // var user = UserManager.FindByName(UserName);
            ////  var user = _context.Users.Single(u => u.Id ==id.Id);
            //var userid = User.Identity.GetUserId();
            //var cart = _context.Carts.Where(c => c.UserId == userid).First();
            //var products = _context.Products.ToList();
            //var Categories = _context.Categories.ToList();
            //var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
            ////var role = _context.Roles.Where(r => r.Id == Role.Id).First();
            //var Roles = _context.Roles.ToList();
            //var viewmodel = new ProductCartViewHome
            //{
            //    Cart = cart,
            //    CatrtProduct = productscart,
            //    Categoris = Categories,
            //    Products = products,
            //    Roles = Roles,
            //  //  Users = user
            //    //  Role=role
            //};



            //var UserManager = new UserManager<ApplicationUser>
            //    (new UserStore<ApplicationUser>(_context));

            //try
            //{
            //    var user = UserManager.FindByName(UserName);
            //    UserManager.AddToRole(user.Id, Role.Name);
            //    _context.SaveChanges();
            //}catch
            //{ }
            ////var account = new AccountController();//< --NULL REFERENCE EXCEPTION OBJECT NOT SET TO INSTANCE
            ////account.UserManager.AddToRole(user.Id, Role.Name);

            ////////////////

            //// var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());



            //// var roleManager = new RoleManager<IdentityRole>(roleStore);
            ////await roleManager.CreateAsync(new IdentityRole());
            /////    await UserManager.AddToRoleAsync(userid,role.Name);

            ///////////////

            ////var roleStore = new RoleStore<IdentityRole>(_context);
            ////var roleManager = new RoleManager<IdentityRole>(roleStore);

            ////var userStore = new UserStore<ApplicationUser>(_context);
            ////var userManager = new UserManager<ApplicationUser>(userStore);
            ////userManager.AddToRole(user.Id, Role.Id);
            ////
            //// var userinDB = _context.Users.Single(c => c.Id == id.Id);


            ////if (user != null)
            ////{
            ////    var result = UserManager.AddToRole(user.Id, Role.Name);
            ////    if (result.Succeeded)
            ////    {
            ////        _context.SaveChanges();
            ////        return RedirectToAction("Index");
            ////    }
            ////    else
            ////        return HttpNotFound();
            ////    //   await UserManager.AddToRoleAsync(userid, viewmodel.Role.Name);

            ////}
            ////else
            ////{
            ////    return HttpNotFound();
            ////}



            ////  ViewBag.ResultMessage = "Role created successfully !";

            //// prepopulat roles for the view dropdown


            ////return View("ManageUserRoles");
           

        }

        public ActionResult Details(string id)
        {
            if (User.IsInRole("Admin"))
            {
                var viewmodel = new ProductCartViewHome();


                var userid = User.Identity.GetUserId();
                var cart = _context.Carts.Where(c => c.UserId == userid).First();
                var user = _context.Users.SingleOrDefault(p => p.Id == id);
                var Categories = _context.Categories.ToList();
                var productscart = _context.CartsProducts.Include(cp => cp.Product).Where(cp => cp.CartId == cart.id).ToList();
                var orders = _context.Orders.Where(or => or.userid == id).ToList();



                viewmodel.Cart = cart;
                viewmodel.Users = user;
                viewmodel.Categoris = Categories;
                viewmodel.CatrtProduct = productscart;
                viewmodel.Orders = orders;


                return View(viewmodel);
            }
            else
                return HttpNotFound();
        }
        

        }
}