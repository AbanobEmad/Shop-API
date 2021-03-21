using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class SellerViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        // GET: Seller
        public ActionResult AddSeller()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSeller(Seller seller)
        {
            if (ModelState.IsValid)
            {

                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Show");

            }
            return View();
        }
        public ActionResult Show()
        {
            List<Seller> sellers = db.Sellers.ToList();
            return View(sellers);
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}