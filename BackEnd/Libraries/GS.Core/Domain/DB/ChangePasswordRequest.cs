using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DB
{
    public class ChangePasswordRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string newPassword { get; set; }

    }
    public class ResetPasswordRequest
    {
        public string username { get; set; }
        public string newPassword { get; set; }

    }
    public class PasswordRequestModel
    {
        public string username { get; set; }
        public string newPassword { get; set; }
        public string password { get; set; }
        public string TokenSSO { get; set; }

    }
}
