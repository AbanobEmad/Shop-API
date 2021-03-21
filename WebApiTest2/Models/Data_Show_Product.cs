using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class Data_Show_Product
    {
        public int ID { get; set; }
        [Display(Name = "Description/وصف المنتج")]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Approval { get; set; }
        public float Discount { get; set; }
        public bool Offer { get; set; }
        public float percent { get; set; }
        public int Category_ID { get; set; }
        public int Seller_ID { get; set; }
        public int Type_ID { get; set; }
        public bool Save { get; set; }
        public float Shipping { get; set; }
        public bool Show_Home { get; set; }
        public string Seller_Name { get; set; }
        public string Cate_Name { get; set; }
        public List<string> LinkIamge { get; set; }
    }
}