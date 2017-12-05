using FlowerShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Controllers
{
    public class FlowersController : Controller
    {

        private FlowerDBEntities db = new FlowerDBEntities();

        // GET: Flowers
        public ActionResult Index()
        {
            return View(db.Flowers.ToList());
        }

        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Specials()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            else
            {
                ViewBag.Username = Session["Username"];
                return View("Welcome");
            }
            
        }

        public ActionResult Welcome()
        {
            ViewBag.Username = Session["Username"];
            return View();
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Username"] = null;
            return View("MyAccount");
        }



        [HttpPost]
        public ActionResult MyAccount(String username, String password)
        {
            var users = from user in db.Users
                        where user.Username.Equals(username)
                        select user;
            User u = users.FirstOrDefault();
            if (u == null)
            {
                return View("Register");
            }
            else if (!u.Password.Equals(password))
            {
                return View("MyAccount");
            }
            else
            {
                Session["ID"] = u.ID;
                Session["Username"] = u.Username;
                return RedirectToAction("Index");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return View("MyAccount");
        }

        [UserAuthorize]
        public ActionResult Cart()
        {
            decimal total = 0;
            List<CartFlower> cfs = db.CartFlowers.ToList();
            foreach (CartFlower cf in cfs)
            {
                total += cf.Count * cf.Price;
            }
            ViewBag.Total = total;
            return View(db.CartFlowers.ToList());
        }


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower f = db.Flowers.Find(id);
            if (f == null)
            {
                return HttpNotFound();
            }
            return View(f);
        }

        [HttpPost]
        public String AddToCart(int id)
        {
            if (Session["ID"] != null)
            {
                int userId = ((int)Session["ID"]);

                var cfs = from v in db.CartFlowers
                          where v.FlowerId == id && v.UserId == userId
                          select v;

                CartFlower c = cfs.FirstOrDefault();
                if (c == null)
                {
                    CartFlower cf = new CartFlower();
                    cf.FlowerId = id;
                    cf.UserId = userId;
                    cf.Count = 1;
                    Flower f = db.Flowers.Find(id);
                    cf.Price = f.Price;
                    cf.Img = f.Img;
                    cf.ProductName = f.ProductName;
                    db.CartFlowers.Add(cf);
                }
                else
                {
                    c.Count += 1;
                    db.Entry(c).State = EntityState.Modified;
                }
                db.SaveChanges();
                return "添加购物车成功";
            }
            return "请先登录";
        }

        public ActionResult EditCart(int? id)
        {
            CartFlower cf = db.CartFlowers.Find(id);
            if (cf == null)
            {
                return HttpNotFound();
            }
            return View(cf);
        }

        [HttpPost]
        public String ChangeCount(int id, int count)
        {
            CartFlower cf = db.CartFlowers.Find(id);
            if (cf != null)
            {
                if (cf.Count != count)
                {
                    cf.Count = count;
                    db.Entry(cf).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return "OK";
            }
            return "ERROR";
        }

        public ActionResult CartDelete(int? id)
        {
            CartFlower cf = db.CartFlowers.Find(id);
            if (cf != null)
            {
                db.CartFlowers.Remove(cf);
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }
    }
}