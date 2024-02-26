using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class RegisterModel
    {

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Useremail { get; set; }

        public string Userpassword { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime LastLoginTime { get; set; }

    }
}
