using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FlowerShop.Models
{
    public class FlowerDBEntities : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Flower> Flowers { get; set; }

        public DbSet<CartFlower> CartFlowers { get; set; }
    }
}