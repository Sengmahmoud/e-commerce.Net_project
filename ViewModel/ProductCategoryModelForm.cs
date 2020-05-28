using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.ViewModel
{
    public class ProductCategoryModelForm
    {
        public IEnumerable<Category> Categories { get; set; }
        public Product Product { get; set; }
    }
}