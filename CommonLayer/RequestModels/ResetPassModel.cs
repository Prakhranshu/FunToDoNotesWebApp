using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class ResetPasswordModel
    {
        public string new_password { get; set; }
        public string confirm_password { get; set; }
    }
}
