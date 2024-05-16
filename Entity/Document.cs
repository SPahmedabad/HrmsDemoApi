using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Document
    {
        public int emp_id { get; set; }
        public string doc_type_id { get; set; }
        public string filepath { get; set; }
        public string filename { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
    }
}
