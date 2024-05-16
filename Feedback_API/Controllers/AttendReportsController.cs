using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library;
using BL;
using Entity;


namespace Feedback_API.Controllers
{
    public class AttendReportsController : ApiController
    {
        // Author -> Yaksh Maishery
        // Fetch Data from the tbl_attend_table
        [Route("api/AttendReports/getAttends")]
        [HttpPost]
        public HttpResponseMessage getAttends(attend_entity en)
        {
            Response_entity res_en = new Response_entity();
            try
            {
                res_en.ArrayOfResponse = BL.attend_bl.getAttends(en);
                if(res_en.ArrayOfResponse.Rows.Count > 0)
                {
                    res_en.status = "success";
                    res_en.message = "There is some data";
                }
                else
                {
                    res_en.status = "failed";
                    res_en.message = "There is no data";
                }
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrorLog("class -> AttendReportsController, method -> getAttends\nMessage :" + ex.Message + "\nHResult:" + ex.HResult);
            }
            return Request.CreateResponse(HttpStatusCode.OK, res_en);
        }
    }
}
