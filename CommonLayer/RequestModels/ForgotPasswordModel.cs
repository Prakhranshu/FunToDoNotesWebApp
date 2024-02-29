using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }

    }
}
