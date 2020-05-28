﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public  Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int qnt { get; set; }
    }
}