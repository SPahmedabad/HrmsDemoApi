using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// Author -> Saddam Shaikh
    public class Emp_Of_Month_Entity
    {
        public int id { get; set; }
        public string title { get; set; }
        public string emp_name { get; set; }
        public string dept_name { get; set; }
        public DateTime eom_date { get; set; }
        public string img_path { get; set; }
        public string designation { get; set; }
        public string flag { get; set; }
    }
}

