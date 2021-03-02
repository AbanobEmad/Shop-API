using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class DataUser
    {
        public int ID { get; set; }
        public float Cash { get; set; }
        public long point { get; set; }
        public string Name { get; set; }
        public string User_ID { get; set; }
        public string Name_seconde { get; set; }

        public string Address { get; set; }
        public string Phone_Number { get; set; }
        [ForeignKey("User_ID")]
        public virtual IdentityUser User { get; set; }
    }
}