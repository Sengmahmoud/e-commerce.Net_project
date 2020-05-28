using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class CartProduct
    {

        
        public Cart  Cart{ get; set; }
        public int CartId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int qnt { get; set; }
    }
}