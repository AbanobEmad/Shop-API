using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class SizeViewController : Controller
    {
        // GET: SizeView
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddSize()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSize(Size size)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    db.Sizes.Add(size);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
            return RedirectToAction("ShowSize");
        }
        public ActionResult ShowSize()
        {
            List<Size> sizes = db.Sizes.ToList();
            return View(sizes);
        }
        public ActionResult DeleteSize(int id)
        {
            Size size = db.Sizes.FirstOrDefault(S => S.ID == id);
            db.Sizes.Remove(size);
            db.SaveChanges();
            return RedirectToAction("ShowSize");
        }
    }
}