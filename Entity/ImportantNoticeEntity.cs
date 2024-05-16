using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entity
{
    //entity for table ImportantNotice
    public class ImportantNoticeEntity
    {
        public int id { get; set; }
        public string flag { get; set; } // insert or update or delete or getall or lastnotice
        public string notice { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
