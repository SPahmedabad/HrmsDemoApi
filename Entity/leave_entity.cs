using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class leave_entity
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Report_manger { get; set; }
        public string Leaves { get; set; } = "";
        public string WorkingDays { get; set; } = "";
        public string Employee_Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Status { get; set; }
        public string flag { get; set; } = ""; // for fetch the specific data
        //public string Reporting_Person { get; set; }
    }
}
