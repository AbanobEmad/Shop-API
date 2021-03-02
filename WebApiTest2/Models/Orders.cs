using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Orders")]
    public class Orders
    {
        public int ID { get; set; }
        public string User_ID { get; set; }
        public string Stuts_AR{ get; set; }
        public string Stuts_EN { get; set; }
        public float Shipping { get; set; }
        public double PA { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User_ID")]
        public virtual IdentityUser User { get; set; }
        public ICollection<Single_Order> Single_Orders { get; set; }
    }
}