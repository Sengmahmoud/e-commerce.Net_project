using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace e_commerce.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int Quantity { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Product>Products { get; set; }
    }
}