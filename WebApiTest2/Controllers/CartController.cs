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
    [Authorize]
    public class CartController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("Cart")]
        public IHttpActionResult Getcart()
        {
            string User_Id = User.Identity.GetUserId();
            List<Models.Cart> carts;
            carts = db.Carts.Where(C => C.User_ID == User_Id).ToList();
            List<Models.DbCartOfProduct> dpCarts = new List<Models.DbCartOfProduct>();
            DbAllCarts dbAllCart = new DbAllCarts();
            float shipping = 0;
            double price_After = 0;
            foreach (var c in carts)
            {
                Models.DbCartOfProduct dpCart = new DbCartOfProduct();
                Models.Product product = db.Products.FirstOrDefault(P=>P.ID==c.Product_ID);
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
                if (c.Size_Id != null)
                {
                    Models.SizeOFProduct sizeOFProduct = db.SizeOFProducts.FirstOrDefault(S => S.Size_ID == c.Size_Id&&S.Product_ID==c.Product_ID);
                    Models.Size size = db.Sizes.FirstOrDefault(S => S.ID == c.Size_Id);
                    dpCart.M_C = sizeOFProduct.Max_C;
                    dpCart.S_N = size.size;
                }
                dpCart.p =FillDpproduct(product, saved);
                dpCart.Id = c.ID;
                dpCart.S_Id = c.Size_Id;
                dpCart.C = c.Count;
                shipping = Math.Max(shipping, product.Shipping);
                price_After += ((product.Price - product.Discount)* c.Count);
                dpCarts.Add(dpCart);
            }
            dbAllCart.Cart = dpCarts;
            dbAllCart.S = shipping;
            dbAllCart.PA = price_After;
            return Ok(dbAllCart);
        }
        [Route("Cart")]
        public IHttpActionResult PostCart(DpCart cart)
        {
            string User_Id = User.Identity.GetUserId();
            Models.Cart cart1 = new Cart();
            Models.Cart cartChek = db.Carts.FirstOrDefault(C => C.Product_ID == cart.P_Id && C.User_ID == User_Id&&C.Size_Id==cart.S_Id);
            if (cartChek == null)
            {
                cart1.Product_ID = cart.P_Id;
                cart1.Size_Id = cart.S_Id;
                cart1.Count = cart.C;
                cart1.User_ID = User_Id;
                db.Carts.Add(cart1);
            }
            else
            {
                cartChek.Count = cart.C;
            }
            db.SaveChanges();
            Models.Result_Message result_ = new Result_Message();
            result_.Status = true;
            result_.Message = "The product is Added to Cart";
            return Created("", result_);
        }
        [Route("DeleteCart/{id}")]
        public IHttpActionResult DeleteCart(int id)
        {
            Models.Cart cart = db.Carts.FirstOrDefault(C => C.ID == id);
            if(cart==null)
            {
                return NotFound();
            }
            db.Carts.Remove(cart);
            db.SaveChanges();
            Returnupdate result_ = new Returnupdate();
            result_ = FillPriceUpdate();
            result_.Status = true;
            result_.Message = ("The products is Updated in Cart the Count is = ");
            result_.C = cart.Count;
            return Ok(result_);

        }
        [Route("DeleteCarts")]
        public IHttpActionResult DeleteCart()
        {
            string User_Id = User.Identity.GetUserId();
            List<Models.Cart> carts = db.Carts.Where(C => C.User_ID == User_Id).ToList();
            if (carts == null||carts.Count==0)
            {
                return NotFound();
            }
            foreach(var c in carts)
            {
                db.Carts.Remove(c);
            }
            db.SaveChanges();
            Models.Result_Message result_ = new Result_Message();
            result_.Status = true;
            result_.Message = "The products is Deleted from Cart";
            return Ok(result_);
        }
        [Route("Update/Cart")]
        public IHttpActionResult PutCart(UpdateCart updateCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Models.Cart cart = db.Carts.FirstOrDefault(C => C.ID == updateCart.Id);
            if(cart == null)
            {
                return NotFound();
            }
            cart.Count = updateCart.C;
            db.SaveChanges();
            Returnupdate result_ = new Returnupdate();
            result_ = FillPriceUpdate();
            result_.Status = true;
            result_.Message = ("The products is Updated in Cart the Count is = ");
            result_.C = cart.Count;
            return Ok(result_);
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
        Returnupdate FillPriceUpdate()
        {
            string User_Id = User.Identity.GetUserId();
            List<Models.Cart> carts;
            carts = db.Carts.Where(C => C.User_ID == User_Id).ToList();
            double price_After = 0;
            float shipping = 0;
            Returnupdate result_ = new Returnupdate();
            foreach (var c in carts)
            {
                Models.DbCartOfProduct dpCart = new DbCartOfProduct();
                Models.Product product = db.Products.FirstOrDefault(P => P.ID == c.Product_ID);
                price_After += ((product.Price - product.Discount) * c.Count);
                shipping = Math.Max(shipping, product.Shipping);
            }
            result_.PA = price_After;
            result_.S = shipping;
            return result_;
        }
    }
    public class UpdateCart
    {
        public int Id{ get; set; }
        public int C { get; set; }
    }
    public class Returnupdate: Result_Message
    {
        public int C { get; set; }
        public double PA { get; set; }
        public float S { get; set; }

    }
}
