using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Response_entity
    {
        public string status { get; set; }
        public string message { get; set; }
        public DataTable ArrayOfResponse { get; set; }
    }
}
