using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class CategoryViewController : Controller
    {
        // GET: CategoryView
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult AddGategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddGategory(CatogoryData catogoryData)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Category category = new Category();

                    category.Category_Name_AR = catogoryData.Cat_Name_AR;
                    category.Category_Name_EN = catogoryData.Cat_Name_En;
                    category.Show_Home = catogoryData.Home_Show;
                    category.Category_Image_Path = uploadImage(catogoryData.file);
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

            }
            return RedirectToAction("ShowCategory"); 
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ShowCategory()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }
        public ActionResult ShowSingleCate(int id)
        {
            Category category = db.Categories.FirstOrDefault(C => C.ID == id);
            ViewBag.id = id;
            CatogoryData catogoryData = new CatogoryData();
            catogoryData.Cat_Name_AR = category.Category_Name_AR;
            catogoryData.Cat_Name_En = category.Category_Name_EN;
            catogoryData.Image_Path = category.Category_Image_Path;
            catogoryData.Home_Show = category.Show_Home;
            return View(catogoryData);
        }
        public ActionResult SaveEdit(int id,CatogoryData category)
        {
            Category category1 = db.Categories.FirstOrDefault(C => C.ID == id);
            category1.Category_Name_AR = category.Cat_Name_AR;
            category1.Category_Name_EN = category.Cat_Name_En;
            category1.Show_Home = category.Home_Show;

            if(category.file!=null&&category.file.ContentLength>0)
            {
                category1.Category_Image_Path = uploadImage(category.file);
            }
            db.SaveChanges();
            return RedirectToAction("ShowCategory");
        }
        string uploadImage(HttpPostedFileBase file)
        {
            string result;
            string date = DateTime.Now.Date.ToShortDateString();
            string time = DateTime.Now.TimeOfDay.ToString();
            if (file != null && file.ContentLength > 0)
                try
                {
                    string newFileName = Path.Combine(Path.GetDirectoryName(file.FileName)
                               , string.Concat(Path.GetFileNameWithoutExtension(file.FileName)
                               , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                               , Path.GetExtension(file.FileName)
                               )
                );
                    string path = Path.Combine(Server.MapPath("~/images"), newFileName);
                    path = path.Trim();
                    file.SaveAs(path);
                    result = "http://thequickapps.info/images/" + newFileName;
                    ViewBag.s = "scusses";
                }
                catch (Exception ex)
                {
                    result = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                result = "You have not specified a file.";
            }
            return result;
        }
    }
}