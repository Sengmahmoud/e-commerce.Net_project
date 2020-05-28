using e_commerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_commerce.Startup))]
namespace e_commerce
{
    public partial class Startup
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreatDefualtUserRole();
        }
        public void CreatDefualtUserRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "root099@gmail.com";
                user.Email = "root099@gmail.com";
                var check = userManger.Create(user, "Root_123");
                if (check.Succeeded)
                {
                    userManger.AddToRole(user.Id, "Admin");
                    var carts = new Cart { UserId = user.Id, Quantity = 1 };
                    _context.Carts.Add(carts);
                    _context.SaveChanges();
                }

            }
        }
    }
}
