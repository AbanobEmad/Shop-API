using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApiTest2.Controllers
{
    public class UploadImageController : ApiController
    {
        [Route("Image")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                int i = 0;
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[i++];
                    var filePath = HttpContext.Current.Server.MapPath("~\\images/" + postedFile.FileName);
                    var imagePath = "http://abanoubemad-001-site1.htempurl.com/images/" + postedFile.FileName;
                    postedFile.SaveAs(filePath);
                    docfiles.Add(imagePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
