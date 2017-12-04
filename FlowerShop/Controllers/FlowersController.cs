using FlowerShop.Models;
using System;
using System.Collections.Generic;
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
                return View("Index");
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
            return View();
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
    }
}