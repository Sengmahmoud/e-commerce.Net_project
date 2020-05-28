using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name ="Frist Name") ]
        public string FristName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderProduct>OrderProducts { get; set; }
        public DbSet<CartProduct> CartsProducts { get; set; }


        // public DbSet<IdentityUserRole> userrole { get; set; }
        // public DbSet<ApplicationUserManager> usermanger { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartProduct>().HasKey(pc => new
            {
                pc.CartId,
                pc.ProductId
            });
            //  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderProduct>().HasKey(op => new
            {
                op.OrderId,
                op.ProductId

            });
            
        }

       



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}