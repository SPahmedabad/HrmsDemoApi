using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Feedback_API.Controllers
{
    public class FeedbackController : ApiController
    {
        //author Ravi vaghela
        [Route("api/Feedback/save")]
        [HttpPost]
        public HttpResponseMessage save(FeedbackFormEntity obj)
        {
            int result = 0;
            HttpResponseMessage response = new HttpResponseMessage();
            MessageEntity mobj = new MessageEntity();
            try
            {

                //insert in feedback table 
                Operation dpOp = new Operation();
                 result = dpOp.Feedback_save(obj);
      
                //insert answer of rating questions
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception exe)
            {

                mobj.Message = exe.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return response;
        }
    }
}
