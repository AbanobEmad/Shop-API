using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest2.Models
{
    public class CatogoryData
    {
        public HttpPostedFileBase file { get; set; }
        public string Cat_Name_En { get; set; }
        public string Cat_Name_AR { get; set; }
        public bool Home_Show { get; set; }

        public string Image_Path { get; set; }
    }
}