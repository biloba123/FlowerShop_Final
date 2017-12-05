using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerShop.Models
{
    public class CartFlower
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FlowerId { get; set; }

        public int Count { get; set; }
    }
}