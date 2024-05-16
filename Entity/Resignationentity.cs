using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Resignationentity
    {
        public DateTime date_of_resign { get; set; }
        public DateTime last_day_work { get; set; }
        public string reason_of_leave { get; set; }
        public int Employee_code { get; set; }
        public string flag { get; set; }
        public int ID { get; set; }
        public string Employee_Name { get; set;}
    }
}
