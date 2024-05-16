using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //Author -> Saddam Shaikh
    public class DepartmentEntity
    {
        public int dept_id { get; set; }

        public string dept_name { get; set; }
    }
    public class AdminEntity
    {
        //Admin View Data Entity
        public int dpt_id { get; set; }
        public int md_id { get; set; }
        public string flag { get; set; }


    }
}
