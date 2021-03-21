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
    public class OrderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Route("Order")]
        public IHttpActionResult PostOrder(Data_User_Order data)
        {
            try
            {
                DateTime date = DateTime.Now.Date;
                string User_Id = User.Identity.GetUserId();
                double Price_After = 0;
                float shipping = 0;
                List<Models.Cart> carts = db.Carts.Where(C => C.User_ID == User_Id).ToList();
                Message_Order result_ = new Message_Order();
                Models.Orders orders = new Orders();
                DataUser dataUser = db.DataUsers.FirstOrDefault(U => U.User_ID == User_Id);
                dataUser.Name_seconde = data.Name;
                dataUser.Address = data.Address;
                dataUser.Phone_Number = data.Phone;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                orders.Date = date;
                orders.User_ID = User_Id;
                orders.Stuts_AR = "لم يتم الرد عليه";
                orders.Stuts_EN = "not answered";
                db.Orders.Add(orders);
                db.SaveChanges();
                foreach (var item in carts)
                {
                    Models.Single_Order single_Order = new Single_Order();
                    single_Order.Count = item.Count;
                    single_Order.Product_ID = item.Product_ID;
                    single_Order.Size_Id = item.Size_Id;
                    single_Order.Orders_Id = orders.ID;
                    Models.Product product = db.Products.FirstOrDefault(P => P.ID == item.Product_ID);
                    if(product!=null)
                    {
                        Price_After+= ((product.Price - product.Discount) * item.Count);
                        shipping = Math.Max(shipping, product.Shipping);
                    }
                    db.Single_Orders.Add(single_Order);
                    db.Carts.Remove(item);
                }
                orders.Shipping = shipping;
                orders.PA = Price_After;
                db.SaveChanges();
                result_.Status = true;
                result_.Message = "The Order is done";
                result_.Id = orders.ID;
                return Ok(result_);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Order")]
        public IHttpActionResult GetOrder()
        {
            string User_Id = User.Identity.GetUserId();
            List<Models.Orders> orders = db.Orders.Where(O => O.User_ID == User_Id&&O.Stuts_EN!="Cansel").ToList();
            List<DbAllOrders> dbAllOrders = new List<DbAllOrders>();
            foreach(var o in orders)
            {
                List<Models.Single_Order> single_Orders = db.Single_Orders.Where(O => O.Orders_Id == o.ID).ToList();
                List<DpOrder> dpOrders = new List<DpOrder>();
                DbAllOrders dbAll = new DbAllOrders();
                foreach(var item in single_Orders)
                {
                    Models.DpOrder dpOrder = new DpOrder();
                    Models.Product product = db.Products.FirstOrDefault(P => P.ID == item.Product_ID);
                    Models.Save_Product save_Product = db.Save_Products.FirstOrDefault(S => S.Product_ID == product.ID && S.User_ID == User_Id);
                    bool saved = false;
                    if (save_Product != null)
                    {
                        saved = true;
                    }
                    if (item.Size_Id != null)
                    {
                        Models.SizeOFProduct sizeOFProduct = db.SizeOFProducts.FirstOrDefault(S => S.ID == item.Size_Id);
                        Models.Size size = db.Sizes.FirstOrDefault(S => S.ID == sizeOFProduct.Size_ID);
                        dpOrder.S_N = size.size;
                    }
                    dpOrder.p = FillDpproduct(product, saved);
                    dpOrder.Id = item.ID;
                    dpOrder.Id = item.ID;
                    dpOrder.S_Id = item.Size_Id;
                    dpOrder.C = item.Count;
                    dpOrders.Add(dpOrder);
                }
                dbAll.id = o.ID;
                dbAll.orders = dpOrders;
                dbAll.S_AR = o.Stuts_AR;
                dbAll.S_EN = o.Stuts_EN;
                dbAll.S = o.Shipping;
                dbAll.PA = o.PA;
                dbAll.Date = o.Date.ToShortDateString();
                dbAllOrders.Add(dbAll);
            }
            dbAllOrders = dbAllOrders.OrderBy(X => X.Date).ToList();

            return Ok(dbAllOrders);
        }
        [Route("Update/Order")]
        public IHttpActionResult PutOrder(Model_Update update)
        {
            string user_Id= User.Identity.GetUserId();
            Message_Order result_ = new Message_Order();
            Models.Orders orders = db.Orders.FirstOrDefault(O => O.ID == update.id && O.User_ID == user_Id);
            if (update.S=="Cansel"&&orders!=null)
            {
                orders.Stuts_AR = "تم الغاء الطلب";
                orders.Stuts_EN = "Cansel";
                db.SaveChanges();
                result_.Status = true;
                result_.Message = "The Order is Updated";
                result_.Id = orders.ID;
                return Ok(result_);
            }
            return BadRequest();
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
    public class Message_Order : Result_Message
    {
        public int Id { get; set; }
    }
    public class Model_Update
    {
        public int id { get; set; }
        public string S { get; set; }
    }
}
