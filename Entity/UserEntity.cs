using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserEntity
    {
        public string role { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class logEntity
    {
        public string email_id { get; set; }
        public string otp { get; set; }
        public object Password { get; set; }
    }
}

