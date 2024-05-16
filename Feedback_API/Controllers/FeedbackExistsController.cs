using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;
using Library;

namespace Feedback_API.Controllers
{
    public class FeedbackExistsController : ApiController
    {
        [HttpPost]
        [Route("api/FeedbackExists/Chk_log_exist")]
        public HttpResponseMessage Chk_log_exist(EmployeeEntity ent)
        {
            Operation obj = new Operation();
            return Request.CreateResponse(HttpStatusCode.OK, obj.log_exists(ent));
        }
    }
}
