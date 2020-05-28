using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_commerce.Models;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e_commerce.ViewModel
{
    public class ProductCartViewHome
    {
        public Cart Cart { get; set; }
        public IEnumerable<Product>Products { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category>Categoris { get; set; }
        public IEnumerable<CartProduct> CatrtProduct { get; set; }
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public ApplicationUser Users { get; set; }
        public IEnumerable<ApplicationUser>users { get; set; }
        public IEnumerable<OrderProduct>OrderProduct { get; set; }
        public IEnumerable<Order>Orders { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
     
                                           //  public IEnumerable<IdentityUserRole> UserRole { get; set; }
                                           //  public IdentityRole Role { get; set; }
                                           //[Required]
                                           //[Display(Name = "Email")]
                                           //[EmailAddress]
                                           //public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }
}