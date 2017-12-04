using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowerShop.Models
{
    public class Flower
    {
        public int Id { get; set; }

        [Display(Name = "图片")]
        public string Img { get; set; }

        [Display(Name = "花名")]
        public string ProductName { get; set; }

        [Display(Name = "单价")]
        public decimal Price { get; set; }

        [Display(Name = "介绍")]
        public string Intro { get; set; }
    }
}