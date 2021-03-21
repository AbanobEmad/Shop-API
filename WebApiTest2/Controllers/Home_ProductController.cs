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
    public class Home_ProductController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string User_Id;
        [Route("Home/Products")]
        public IHttpActionResult GetProductToHome()
        {
            bool auth = User.Identity.IsAuthenticated;

            if (auth)
            {
                User_Id = User.Identity.GetUserId();
            }
            List<Product> products = new List<Product>();
            products = db.Products.Where(C => C.Show_Home == true && C.Approval == true).ToList();

            List<DpProduct> DpProducts = new List<DpProduct>();

            foreach (var item in products)
            {
                bool saved = false;
                if (auth == true)
                {
                    Models.Save_Product save_Product = db.Save_Products.FirstOrDefault(S => S.Product_ID == item.ID && S.User_ID == User_Id);
                    if (save_Product != null)
                    {
                        saved = true;
                    }
                }
                DpProducts.Add(FillDpproduct(item, saved));
            }
            return Ok(DpProducts);
        }
        [Route("Home/Category")]
        public IHttpActionResult GetCategoryToHome()
        {

            List<CategoryHome> categoryHomes = new List<CategoryHome>();
            List<Category> catogories = db.Categories.Where(C => C.Show_Home == true).ToList();
            foreach(var item in catogories)
            {
                CategoryHome categoryHome = new CategoryHome();
                categoryHome.Id = item.ID;
                categoryHome.I_P = item.Category_Image_Path;
                categoryHomes.Add(categoryHome);
            }
            return Ok(categoryHomes);
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
            dpProduct.Show_Home = product.Show_Home;
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
    public class CategoryHome
    {
        public int Id { get; set; }
        public string I_P { get; set; }
    }
}
