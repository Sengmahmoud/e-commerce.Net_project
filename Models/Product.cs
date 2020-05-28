using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        public string Name { get; set; }
        ///[DataType(DataType.Upload)]
        //[Required]
        [Display(Name ="Product Image")]
        public string img { get; set; }
        [Required]
        [Display(Name ="Product Price")]
        public decimal price { get; set; }
        [Required]
        [Display(Name ="Product available Quantity")]
        public int qnt { get; set; }
       
        public Category Category { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    
       }

        
}