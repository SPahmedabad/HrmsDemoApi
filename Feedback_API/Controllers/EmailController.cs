using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;
using Entity;
using Library;
namespace Feedback_API.Controllers
{
    public class EmailController : ApiController
    {
        //author chirag
        /// <summary>
        /// here we fetch data from api.
        /// and send entity to Main BL.
        /// this controller is used for send mail to trainee..
        /// </summary>
        [HttpPost]
        [Route("api/Email/SendMail")]
        public HttpResponseMessage SendMail(List<EmployeeEntity> obj_Send_Mail)
        {
            string message = "";
            try
            {
                Operation obj_mail = new Operation();
                message=obj_mail.SendMailToTrainee(obj_Send_Mail);               
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrorLog("Error in EmailController/SendMail() : Message:" + ex.Message + "stacktrace:" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, message);
        }
    }
}
