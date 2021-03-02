using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class productController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string User_Id;
        [Route("Products/{id=-1}")]
        public IHttpActionResult GetProductByGategory(int id)
        {
            bool auth = User.Identity.IsAuthenticated;
            
            if(auth)
            {
                User_Id= User.Identity.GetUserId();
            }
            List<Product> products = new List<Product>();
            if (id==-1)
            {
                 products = db.Products.Where(P=>P.Approval==true).ToList();
            }
            else
            {
                products = db.Products.Where(C => C.Category_TD == id&&C.Approval==true).ToList();
            }
            List<DpProduct> DpProducts = new List<DpProduct>();
            
            foreach (var item in products)
            {
                bool saved = false;
                if (auth==true)
                {
                    Models.Save_Product save_Product = db.Save_Products.FirstOrDefault(S => S.Product_ID == item.ID && S.User_ID == User_Id);
                    if(save_Product!=null)
                    {
                        saved = true;
                    }
                }
                DpProducts.Add(FillDpproduct(item,saved));
            }
            return Ok(DpProducts);
        }
        [Route("Product/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.FirstOrDefault(C => C.ID == id);
            if(product==null)
            {
                return BadRequest("The Id Does not exist ");
            }
            bool auth = User.Identity.IsAuthenticated;
            bool saved = false;
            if (auth)
            {
                User_Id = User.Identity.GetUserId();
                Models.Save_Product save_Product = db.Save_Products.FirstOrDefault(S => S.Product_ID == product.ID && S.User_ID == User_Id);
                if (save_Product != null)
                {
                    saved = true;
                }
            }
            return Ok(FillDpproduct(product,saved));
        }
        [Route("Product")]
        public IHttpActionResult PostProduct(DpProduct dpproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product product = new Product();
            product.Name = dpproduct.Name;
            product.Offer = dpproduct.Offer;
            product.Price = dpproduct.Price;
            product.percent = 0;//dpproduct.percent;
            product.Seller_ID = 1;// dpproduct.Seller_ID;
            product.Description = dpproduct.Description;
            product.Discount = 0;//dpproduct.Discount;
            product.Category_TD = dpproduct.Category_TD;
            product.Type_ID = 1;//dpproduct.Type_ID;
            product.Save = false;
            db.Products.Add(product);
            db.SaveChanges();
            
            foreach(var item in dpproduct.Image_path)
            {
                Image_Path image_Path = new Image_Path();
                Product_Image_path product_Image_Path = new Product_Image_path();
                image_Path.Path = item;
                db.Image_Paths.Add(image_Path);
                db.SaveChanges();
                product_Image_Path.Image_path_ID = image_Path.ID;
                product_Image_Path.Product_ID = product.ID;
                db.Product_Images.Add(product_Image_Path);
                db.SaveChanges();
            }
            
            return Created("", product.ID);
        }
        DpProduct FillDpproduct (Product product,bool save)
        {
            DpProduct DpProducts = new DpProduct();

            DpProduct dpProduct = new DpProduct();
            List<string> image_paths = new List<string>();
            dpProduct.ID = product.ID;
            dpProduct.Name = product.Name;
            dpProduct.Offer = product.Offer;
            dpProduct.percent = product.percent;
            dpProduct.Price = product.Price;
            dpProduct.Seller_ID = product.Seller_ID;
            dpProduct.Save = save;
            dpProduct.Type_ID = product.Type_ID;
            dpProduct.Category_TD = product.Category_TD;
            dpProduct.Description = product.Description;
            dpProduct.Discount = product.Discount;
            List<Product_Image_path> product_images = db.Product_Images.Where(I => I.Product_ID == product.ID).ToList();
            foreach (var path in product_images)
            {
                Image_Path image_Path = db.Image_Paths.FirstOrDefault(I => I.ID == path.Image_path_ID);
                image_paths.Add(image_Path.Path);
            }
            List<SizeOFProduct> sizeOFProducts = db.SizeOFProducts.Where(S => S.Product_ID == product.ID).ToList();
            List<Models.DpSize> dpSizes = new List<DpSize>();
            foreach (var size in sizeOFProducts)
            {
                Size size1 = db.Sizes.FirstOrDefault(S => S.ID == size.Size_ID);
                Models.DpSize dpSize = new DpSize();
                dpSize.S_ID = size1.ID;
                dpSize.S = size1.size;
                dpSize.M_C = size.Max_C;
                dpSizes.Add(dpSize);
            }
            dpProduct.SOfP = dpSizes;
            dpProduct.Image_path = image_paths;
            return dpProduct;
        }
    }
}
