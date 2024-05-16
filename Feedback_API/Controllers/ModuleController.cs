using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;
using Entity;

namespace Feedback_API.Controllers
{
    public class ModuleController : ApiController
    {
            [HttpPost]
            [Route("api/Module/Getdata")]
            public HttpResponseMessage Getdata(FeedbackFormEntity en)
            {
                Operation obj = new Operation();
                return Request.CreateResponse(HttpStatusCode.OK, obj.getData(en));
            }

            [HttpPost]
            [Route("api/Module/InsertData")]
            public HttpResponseMessage InsertData(FeedbackFormEntity en)
            {
                Operation obj = new Operation();

                return Request.CreateResponse(HttpStatusCode.OK, obj.saveData(en));
            }
        }
}
