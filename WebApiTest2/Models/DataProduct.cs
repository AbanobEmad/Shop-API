using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DataProduct
    {
        public int ID { get; set; }
        [Display(Name = "Name/الاسم")]
        public string Name { get; set; }
        [Display(Name = "Description/وصف المنتج")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Price/سعر المنتج")]
        public float Price { get; set; }
        [Display(Name = "Approval/اظهار المنتج")]
        public bool Approval { get; set; }
        [Display(Name = "Discount/خصم المنتج ")]
        public float Discount { get; set; }
        [Display(Name = "Offer/هل يوجد عرض ع المنتج ")]
        public bool Offer { get; set; }
        [Display(Name = "percent/خصم المنتج بالنسبة المئوية ")]
        public float percent { get; set; }

        [Display(Name = "Category/نوع المنتج ")]
        public int Category_ID { get; set; }

        [Display(Name = "Seller/البائع  ")]
        public int Seller_ID { get; set; }
        public int Type_ID { get; set; }
        public bool Save { get; set; }
        [Display(Name = "Shipping/سعر التوصيل  ")]

        public float Shipping { get; set; }
        [Display(Name = "Shipping/هل يتم العرض فى الصفحة الرئيسية  ")]

        public bool Show_Home { get; set; }

        public List<HttpPostedFileBase> Files { get; set; }
        public string Seller_Name { get; set; }
        public List<string> LinkIamge { get; set; }

    }
}