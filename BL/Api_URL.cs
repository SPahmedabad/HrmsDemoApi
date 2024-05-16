using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    // Author -> Saddam Shaikh
    public class Api_URL
    {
        // Base Address
        public static string baseAddress = ConfigurationManager.AppSettings["baseAddress"];

        // Secondary Address -> Sub URLs
        public static string subURL_get_Afeed_data = ConfigurationManager.AppSettings["subURL_get_Afeed_data"];
        public static string subURL_GetEmployee = ConfigurationManager.AppSettings["subURL_GetEmployee"];
        public static string subURL_AddEmployee = ConfigurationManager.AppSettings["subURL_AddEmployee"];
        public static string subURL_save_Feedback = ConfigurationManager.AppSettings["subURL_save_Feedback"];
        public static string subURL_Mis = ConfigurationManager.AppSettings["subURL_Mis"];
        public static string subURL_MisDeptListviee = ConfigurationManager.AppSettings["subURL_MisDeptListviee"];
        public static string subURL_ModuleGetdata = ConfigurationManager.AppSettings["subURL_ModuleGetdata"];
        public static string subURL_ModuleInsertData = ConfigurationManager.AppSettings["subURL_ModuleInsertData"];
        public static string subURL_getProfile = ConfigurationManager.AppSettings["subURL_getProfile"];
        public static string subURL_GetQuestion = ConfigurationManager.AppSettings["subURL_GetQuestion"];
        public static string subURL_Training_GetDept = ConfigurationManager.AppSettings["subURL_Training_GetDept"];
        public static string subURL_Training_InsertDept = ConfigurationManager.AppSettings["subURL_Training_InsertDept"];
        public static string subURL_get_Emp_Mail_DT = ConfigurationManager.AppSettings["subURL_get_Emp_Mail_DT"];
        public static string sendmail = ConfigurationManager.AppSettings["sendEmail"];
        public static string SendMailTemplate = ConfigurationManager.AppSettings["sendMailTemplate"];
        public static string feedbackform_url = ConfigurationManager.AppSettings["feedbackform_url"];
        public static string subURL_Mis_form_status = ConfigurationManager.AppSettings["subURL_Mis_form_status"];
        public static string subURL_FeedbackExist = ConfigurationManager.AppSettings["subURL_FeedbackExist"];
        public static string empimgpath = ConfigurationManager.AppSettings["newempimg"];
        public static string refer_cv = ConfigurationManager.AppSettings["refer_cv"];
        public static string SendOTPTemplate = ConfigurationManager.AppSettings["OTPtemplate"];
        public static string SendEmphirarchy = ConfigurationManager.AppSettings["SendEmphirarchy"];
        public static string uploaddocpath = ConfigurationManager.AppSettings["uploaddocpath"];

        //Image Path For Employee of Month
        public static string img_path = ConfigurationManager.AppSettings["file_path"];

        //File Path for Daily Task Sheet Upload and Download
        public static string file_path = ConfigurationManager.AppSettings["daily_sheet_path"];

     
        public static string event_images = ConfigurationManager.AppSettings["event_images"];

    }
}
