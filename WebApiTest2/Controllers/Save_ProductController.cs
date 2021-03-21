using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest2.Models;
using Microsoft.AspNet.Identity;

namespace WebApiTest2.Controllers
{
    [Authorize]
    public class Save_ProductController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route ("Save_P")]
        public IHttpActionResult PostSave(Product_ID product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            string user_id = User.Identity.GetUserId();
            Models.Save_Product saveCheck = db.Save_Products.FirstOrDefault(S => S.Product_ID == product.ID && S.User_ID == user_id);
            if(saveCheck!=null)
            {
                return BadRequest("The product is saved already");
            }
            Models.Save_Product save = new Save_Product();
            save.Product_ID = product.ID;
            save.User_ID = user_id;
            db.Save_Products.Add(save);
            db.SaveChanges();
            Models.Result_Message result_= new Result_Message();
            result_.Status = true;
            result_.Message = "The product is saved";
            return Created("", result_);
        }
        [Route("Delete_P")]
        public IHttpActionResult DeleteProduct(Product_ID product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user_id = User.Identity.GetUserId();
            Models.Save_Product save = db.Save_Products.FirstOrDefault(S => S.Product_ID == product.ID && S.User_ID == user_id);
            if(save==null)
            {
                return NotFound();
            }
            db.Save_Products.Remove(save);
            db.SaveChanges();
            Models.Result_Message result_ = new Result_Message();
            result_.Status = true;
            result_.Message = "The product is Deleted from save";
            return Created("", result_);
        }
        [Route("Saves")]
        public IHttpActionResult GetSave()
        {
            string user_id = User.Identity.GetUserId();
           List< Models.Save_Product> saves = db.Save_Products.Where(S => S.User_ID == user_id).ToList();
            if (saves == null)
            {
                return NotFound();
            }
            List<DpProduct> DpProducts = new List<DpProduct>();
            foreach (var item in saves)
            {
                bool saved = true;
                Models.Product product = db.Products.FirstOrDefault(P => P.ID == item.Product_ID);
                DpProducts.Add(FillDpproduct(product, saved)); ;
            }
            return Ok(DpProducts);
        }
        DpProduct FillDpproduct(Product product, bool save)
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
            dpProduct.Category_TD = product.Category_ID;
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
    public class Product_ID
    {
        public int ID { get; set; }
    }
}
