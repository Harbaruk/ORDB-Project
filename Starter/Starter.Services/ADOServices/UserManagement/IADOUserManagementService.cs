using System;
using System.Collections.Generic;
using System.Text;
using Starter.Services.ADOServices.UserManagement.Models;

namespace Starter.Services.ADOServices.UserManagement
{
    public interface IADOUserManagement
    {
        Users GetById(int id);
        IEnumerable<Users> GetAll();
    }
}