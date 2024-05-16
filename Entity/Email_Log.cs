using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Email_Log
    {
        //public string dept_name { get; set; }
        //public string emp_name { get; set; }
        //public DateTime joining_date { get; set; }
        //public string email { get; set; }
        public int email_log_id { get; set; }
        public string Flag { get; set; }
        public string QueryString { get; set; }
        public char IsSend { get; set; }
    }
}
