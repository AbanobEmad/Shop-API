using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class Data_User_Order
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        
    }
}