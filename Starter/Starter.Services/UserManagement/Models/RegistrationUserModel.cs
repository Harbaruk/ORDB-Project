using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Services.UserManagement.Models
{
    public class RegistrationUserModel
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}