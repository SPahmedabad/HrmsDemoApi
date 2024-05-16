using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EmployeeEntity
    {
        public int emp_id { get; set; }
        public string emp_name { get; set; }
        public string emp_email { get; set; }
        public string querystring { get; set; }
        public DepartmentEntity deptEntity = new DepartmentEntity();
        public ModuleEntity moduleEntity = new ModuleEntity();
        public string TrainerName { get; set; }
        public string isActive { get; set; }
        public DateTime joining_date { get; set; }

        //mail_log entity

        public int email_log_id { get; set; }
        public string Flag { get; set; }
        //public string QueryString { get; set; }
        public char IsSend { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
