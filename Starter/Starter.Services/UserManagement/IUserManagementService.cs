using System;
using System.Collections.Generic;
using System.Text;
using Starter.Services.UserManagement.Models;

namespace Starter.Services.UserManagement
{
    public interface IUserManagementService
    {
        void RegisterUser(RegistrationUserModel registrationUser);
    }
}