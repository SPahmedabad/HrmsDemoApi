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
    public class UtilitiesResignationController : ApiController
    {
        [HttpPost]
        [Route("api/UtilitiesResignation/resign_emp")]
        public HttpResponseMessage resign_emp(Resignationentity en)
        {
            Response_entity res_obj = new Response_entity();
            int count = 0;
            try
            {
                count = Utilitieresign.resing_emp(en);
                if (count >= 1)
                {
                    res_obj.status = "Success";
                    res_obj.message = "Inserted Successfull";
                }
                else
                {
                    res_obj.status = "Failed";
                    res_obj.message = "Not Inserted";
                }
            }
            catch (Exception ex)
            {
                res_obj.status = "errror";
                res_obj.message = "Not Inserted error ";
                Library.InsertLog.WriteErrorLog("Dashboard : newempjoindata : " + ex.Message + "InnerException" + ex.InnerException + "StackTrace :" + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_obj);
        }
    }
}
