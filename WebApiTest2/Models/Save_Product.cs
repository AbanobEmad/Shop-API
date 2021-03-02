using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    [Table("Saved")]
    public class Save_Product
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public string User_ID { get; set; }
        [ForeignKey("Product_ID")]
        public virtual Product product { get; set; }
        [ForeignKey("User_ID")]
        public virtual IdentityUser User { get; set; }
    }
}