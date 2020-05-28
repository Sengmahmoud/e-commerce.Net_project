using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
       
        public ApplicationUser User { get; set; }
        [Required]
        public string userid { get; set; }
    }
}