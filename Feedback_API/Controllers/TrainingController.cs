using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BL;
using Entity;
using System.Web.Http;

namespace Feedback_API.Controllers
{
    public class TrainingController : ApiController
    {
        [HttpPost]
        [Route("api/Training/Getdept")]
        public HttpResponseMessage Getdept(FeedbackFormEntity en_dept)
        {
            Operation obj = new Operation();
            return Request.CreateResponse(HttpStatusCode.OK, obj.get_dept(en_dept));
        }
        [HttpPost]
        [Route("api/Training/InsertDept")]
        public HttpResponseMessage InsertDept(FeedbackFormEntity ent)
        {
            Operation obj = new Operation();
            return Request.CreateResponse(HttpStatusCode.OK, obj.save_dept(ent));
        }
    }
}
