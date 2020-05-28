using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Category
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}