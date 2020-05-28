using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_commerce.Models;

namespace e_commerce.ViewModel
{
    public class CartProductViewModel
    {
        public Cart Cart { get; set; }
        public IEnumerable<CartProduct> Products { get; set; }
    }
}