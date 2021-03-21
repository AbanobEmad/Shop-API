using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class ProductViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static public int product_ID;
        static public int Cate_ID;
        static public List<SizeOFProduct> sizeOFProducts = new List<SizeOFProduct>();
        static public List<SizeName> sizeNames = new List<SizeName>();
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Seller = db.Sellers.ToList();
            ViewBag.Cate = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(DataProduct dataProduct)
        {
            ViewBag.Seller = db.Sellers.ToList();
            ViewBag.Cate = db.Categories.ToList();
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = dataProduct.Name;
                product.Offer = dataProduct.Offer;
                product.percent = dataProduct.percent;
                product.Price = dataProduct.Price;
                product.Seller_ID = dataProduct.Seller_ID;
                product.Shipping = dataProduct.Shipping;
                product.Show_Home = dataProduct.Show_Home;
                product.Description = dataProduct.Description;
                product.Discount = dataProduct.Discount;
                product.Approval = dataProduct.Approval;
                product.Category_ID = dataProduct.Category_ID;
                product.Type_ID = 1;
                db.Products.Add(product);
                db.SaveChanges();
                foreach (var item in dataProduct.Files)
                {
                    Image_Path image_Path = new Image_Path();
                    image_Path.Path = uploadImage(item);
                    db.Image_Paths.Add(image_Path);
                    db.SaveChanges();
                    Product_Image_path product_Image_Path = new Product_Image_path();
                    product_Image_Path.Image_path_ID = image_Path.ID;
                    product_Image_Path.Product_ID = product.ID;
                    db.Product_Images.Add(product_Image_Path);
                    db.SaveChanges();
                }
              
                List<Size> sizes =db.Sizes.ToList();
                ViewBag.size = sizes;
                ViewBag.sizeName = sizeNames;
                ViewBag.IDProduct = product.ID;
                Cate_ID = product.Category_ID;
                return View("AddSize");

            }
            return View("saveSize");
        }

        public ActionResult AddSizeFromProduct(int id)
        {
            List<Size> sizes = db.Sizes.ToList();
            ViewBag.size = sizes;
            ViewBag.sizeName = sizeNames;
            ViewBag.IDProduct =id;
            return View("AddSize");
        }
        public ActionResult AddSize(int id,SizeOFProduct sizeOFProduct)
        {
           
            List<Size> sizes = db.Sizes.ToList();
            SizeName sizeName = new SizeName();
            sizeName.MaxCount = sizeOFProduct.Max_C;
            Size size = db.Sizes.FirstOrDefault(S => S.ID == sizeOFProduct.Size_ID);
            sizeName.Name = size.size;
            sizeNames.Add(sizeName);
            ViewBag.size = sizes;
            ViewBag.sizeName = sizeNames;
            ViewBag.IDProduct = id;
            sizeOFProduct.Product_ID = id;
            sizeOFProducts.Add(sizeOFProduct);
            return View();
        }
        public ActionResult saveSize()
        {
            foreach (var item in sizeOFProducts)
            {
                db.SizeOFProducts.Add(item);
            }
            db.SaveChanges();
            sizeOFProducts.Clear();
            sizeNames.Clear();
            return View();
        }
        public ActionResult ShowProduct()
        {
            List<Product> products = db.Products.ToList();
            List<Data_Show_Product> data_Show_Products = new List<Data_Show_Product>();
            foreach (var item in products)
            {
                data_Show_Products.Add(FillDataShowProduct(item));
            }
            return View(data_Show_Products);
        }
        public ActionResult ShowSingleProduct(int id)
        {
            Product product = db.Products.FirstOrDefault(P => P.ID == id);

            ViewBag.sizeNames = FillSizeName(id);
            return View(FillDataShowProduct(product));
        }
        public ActionResult SaveEdit(int id, Data_Show_Product dataProduct)
        {
            Product product = db.Products.FirstOrDefault(P => P.ID == id);
            product.Name = dataProduct.Name;
            product.Offer = dataProduct.Offer;
            product.percent = dataProduct.percent;
            product.Price = dataProduct.Price;
            product.Shipping = dataProduct.Shipping;
            product.Show_Home = dataProduct.Show_Home;
            product.Description = dataProduct.Description;
            product.Discount = dataProduct.Discount;
            product.Approval = dataProduct.Approval;
            product.Type_ID = 1;
            db.SaveChanges();
            return RedirectToAction("ShowProduct");
        }
       
        public ActionResult Size_Edit(int id)
        {
            SizeOFProduct sizeOFProduct = db.SizeOFProducts.FirstOrDefault(S => S.ID == id);
            Size size = db.Sizes.FirstOrDefault(S => S.ID == sizeOFProduct.Size_ID);
            SizeName sizeName = new SizeName();
            sizeName.Name = size.size;
            sizeName.MaxCount = sizeOFProduct.Max_C;
            sizeName.id = sizeOFProduct.ID;
            return View(sizeName);
        }
       
        public ActionResult Save_Edit_Size(int id,SizeName sizeName)
        {
            SizeOFProduct sizeOFProduct = db.SizeOFProducts.FirstOrDefault(S => S.ID == id);
            sizeOFProduct.Max_C = sizeName.MaxCount;
            db.SaveChanges();
            return RedirectToAction("ShowSingleProduct",new { id=sizeOFProduct.Product_ID});
        }
        public ActionResult Delete_Size(int id)
        {
            SizeOFProduct sizeOFProduct = db.SizeOFProducts.FirstOrDefault(S => S.ID == id);
            int Product_id=sizeOFProduct.Product_ID;
            db.SizeOFProducts.Remove(sizeOFProduct);
            db.SaveChanges();
            return RedirectToAction("ShowSingleProduct", new { id = Product_id });
        }
        private List<SizeName> FillSizeName(int id)
        {
            List<SizeOFProduct> sizeOFProducts = db.SizeOFProducts.Where(S => S.Product_ID == id).ToList();
            List<SizeName> sizeNames1 = new List<SizeName>();
            foreach(var item in sizeOFProducts)
            {
                Size size = db.Sizes.FirstOrDefault(S => S.ID == item.Size_ID);
                SizeName sizeName = new SizeName();
                sizeName.Name = size.size;
                sizeName.MaxCount = item.Max_C;
                sizeName.id = item.ID;
                sizeNames1.Add(sizeName);
            }
            return sizeNames1;
        }
        private Data_Show_Product FillDataShowProduct(Product item)
        {
            Data_Show_Product data_Show_Product = new Data_Show_Product();
            Seller seller = db.Sellers.FirstOrDefault(S => S.ID == item.Seller_ID);
            Category category = db.Categories.FirstOrDefault(C => C.ID == item.Category_ID);
            List<string> images = new List<string>();
            List<Product_Image_path> product_Image_Paths = db.Product_Images.Where(P => P.Product_ID == item.ID).ToList();
            data_Show_Product.ID = item.ID;
            data_Show_Product.Approval = item.Approval;
            data_Show_Product.Category_ID = item.Category_ID;
            data_Show_Product.Description = item.Description;
            data_Show_Product.Discount = item.Discount;
            data_Show_Product.Name = item.Name;
            data_Show_Product.Offer = item.Offer;
            data_Show_Product.percent = item.percent;
            data_Show_Product.Price = item.Price;
            data_Show_Product.Seller_ID = item.Seller_ID;
            data_Show_Product.Shipping = item.Shipping;
            data_Show_Product.Show_Home = item.Show_Home;
            foreach (var path in product_Image_Paths)
            {
                Image_Path image_Path = db.Image_Paths.FirstOrDefault(P => P.ID == path.Image_path_ID);
                images.Add(image_Path.Path);
            }
            data_Show_Product.Seller_Name = seller.Name;
            data_Show_Product.LinkIamge = images;
            data_Show_Product.Cate_Name = category.Category_Name_AR;
            return data_Show_Product;
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