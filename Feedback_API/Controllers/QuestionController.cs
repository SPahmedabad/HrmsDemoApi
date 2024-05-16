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
    public class QuestionController : ApiController
    {
        //author : Ravi vaghela

        //api method for get all questions data from db
        [Route("api/Question/Get")]
        [HttpPost]
        public HttpResponseMessage Get()
        {

            HttpResponseMessage response = new HttpResponseMessage();
            MessageEntity msgobj = new MessageEntity();
            //create datatable object for store data
            DataTable dt = new DataTable();
            try
            {
                Operation dbOp = new Operation();
                //call getQuestions bl method for get data

                dt = dbOp.getQuestions();
                //if data present in datatable then return json data 
                //else return massage no data found
                if (dt.Rows.Count > 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, dt);
                }
                else
                {
                    msgobj.Message = "Not data found ";
                    response = Request.CreateResponse(HttpStatusCode.OK, msgobj);
                }
            }
            catch (Exception exe)
            {
                msgobj.Message = exe.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, msgobj);
            }

            return response;
        }
    }
}
