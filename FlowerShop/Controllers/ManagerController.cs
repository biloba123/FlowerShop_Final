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
    public class ManagerController : Controller
    {
        private FlowerDBEntities db = new FlowerDBEntities();

        // GET: Manager
        [ManagerAuthorize]
        public ActionResult Index()
        {
            return View(db.Flowers.ToList());
        }

        [ManagerAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [ManagerAuthorize]
        [HttpPost]
        public ActionResult Create(Flower f)
        {
            db.Flowers.Add(f);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ManagerAuthorize]
        public ActionResult Edit(int? id)
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

        [ManagerAuthorize]
        [HttpPost]
        public ActionResult Edit(Flower f)
        {
            db.Entry(f).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ManagerAuthorize]
        public ActionResult Delete(int? id)
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

        [ManagerAuthorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Flower f = db.Flowers.Find(id);
            db.Flowers.Remove(f);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            if (username.Equals("admin") && password.Equals("123456"))
            {
                Session["Username"] = username;
                return RedirectToAction("Index");
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            return View("Login");
        }

    }
}