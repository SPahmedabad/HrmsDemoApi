using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Feedback_API.Controllers
{
    public class AdminViewController : ApiController
    {
        /// <summary>
        /// API Method For Getting Feedback Data
        /// On Deparment And Module Name
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AdminView/get_Afeed_data")]
        public HttpResponseMessage get_Afeed_data(FeedbackFormEntity en)
        {
            DataSet ds = new DataSet();
            ds = BL.Operation.get_data(en);

            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

    }
}
