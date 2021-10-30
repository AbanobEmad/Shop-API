using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest2.Controllers
{
    public class VersionController : ApiController
    {
        [Route("Version/{id}")]
        public IHttpActionResult Postversion(int id)
        {
            VersionResullt result_ = new VersionResullt();
            if (id!=1)
            {
                result_.Status = true;
                result_.Update = true;
                result_.Message = "update";
            }
            else
            {
                result_.Status = true;
                result_.Update = false;
                result_.Message = "Not update";
            }
            return Ok(result_);
        }
    }
    public class VersionResullt : Models.Result_Message
    {
        public bool  Update{ get; set; }
    }
}
