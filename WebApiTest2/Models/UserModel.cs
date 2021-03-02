using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class UserModel
    {
        [Phone]
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public float Cash { get; set; }
        public long point { get; set; }
    }
}