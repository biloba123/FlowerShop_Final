using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.Models
{
    public class CartFlower
    {
        public int Id { get; set; }

        public string Img { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }

        public int FlowerId { get; set; }

        public int Count { get; set; }
    }
}