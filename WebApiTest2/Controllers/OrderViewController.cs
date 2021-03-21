using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTest2.Models;

namespace WebApiTest2.Controllers
{
    public class OrderViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ShowNewOrders(int id)
        {
            List<string> status = new List<string>();
            status.Add("in way");
            status.Add("Cansel");
            status.Add("not answered");
            status.Add("done");
            string statu = status[id - 1];
            List<Orders> orders = db.Orders.Where(O => O.Stuts_EN == statu).ToList();
            List<DataOrder> dataorders = new List<DataOrder>();
            foreach (var item in orders)
            {
                DataOrder dataOrder = new DataOrder();
                dataOrder.ID = item.ID;
                dataOrder.Order_Price = item.PA;
                dataOrder.Order_Shapping = item.Shipping;
                DataUser dataUser = db.DataUsers.FirstOrDefault(U => U.User_ID == item.User_ID);
                dataOrder.Address_User = dataUser.Address;
                dataOrder.Name_User = dataUser.Name_seconde;
                dataOrder.Phone_User = dataUser.Phone_Number;
                dataOrder.State = item.Stuts_EN;

                dataorders.Add(dataOrder);
            }
            return View(dataorders);
        }
        public ActionResult ShowoneOrder(int id)
        {
            List<Single_Order> single_orders = db.Single_Orders.Where(O => O.Orders_Id == id).ToList();
            List<DataSingleOrder> dataSingleOrders = new List<DataSingleOrder>();
            Orders order = db.Orders.FirstOrDefault(O => O.ID == id);
            ViewBag.id = id;
            ViewBag.State = order.Stuts_EN;
            foreach (var item in single_orders)
            {
                DataSingleOrder dataSingleOrder = new DataSingleOrder();
                Product product = db.Products.FirstOrDefault(P => P.ID == item.Product_ID);
                Size size = db.Sizes.FirstOrDefault(S => S.ID == item.Size_Id);
                Seller seller = db.Sellers.FirstOrDefault(S => S.ID == product.Seller_ID);
                dataSingleOrder.Count = item.Count;
                dataSingleOrder.ID = item.ID;
                dataSingleOrder.Product_Description = product.Description;
                dataSingleOrder.Product_Name = product.Name;
                dataSingleOrder.Size_Name = size.size;
                dataSingleOrder.Seller_Name = seller.Name;
                dataSingleOrder.Seller_Phone = seller.Phone1;

                dataSingleOrders.Add(dataSingleOrder);
            }
            return View(dataSingleOrders);
        }
        public ActionResult SendOrder(int id)
        {
            Orders order = db.Orders.FirstOrDefault(O => O.ID == id);
            order.Stuts_AR = "فى الطريق";
            order.Stuts_EN = "in way";
            db.SaveChanges();
            return RedirectToAction("ShowNewOrders", new { id = 1 });
        }
        public ActionResult ArrevdedOrder(int id)
        {
            Orders order = db.Orders.FirstOrDefault(O => O.ID == id);
            order.Stuts_AR = "تم التوصيل";
            order.Stuts_EN = "done";
            db.SaveChanges();
            return RedirectToAction("ShowNewOrders", new { id = 3 });
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}