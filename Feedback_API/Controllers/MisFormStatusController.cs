using BL;
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
    public class MisFormStatusController : ApiController
    {
        //Author Jay Patel;
        [HttpPost]
        [Route("api/MisFormStatus/chk_formsstatus")]

        //This method return all trainee_id who return submitted data or not
        public HttpResponseMessage chk_formsstatus(EmployeeEntity log_entity)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = Operation.get_mis_fromdata(log_entity);
            }
            catch (Exception ex)
            {
                Library.InsertLog.WriteErrorLog("Controller :  MisFormStatusController : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
    }
}
