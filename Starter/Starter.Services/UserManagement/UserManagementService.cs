using System;
using System.Collections.Generic;
using System.Text;
using Starter.DAL.Entities;
using Starter.DAL.Infrastructure;
using Starter.Services.UserManagement.Models;
using Starter.Services.Crypto;

namespace Starter.Services.UserManagement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ICryptoContext _cryptoContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserManagementService(ICryptoContext cryptoContext, IUnitOfWork unitOfWork)
        {
            _cryptoContext = cryptoContext;
            _unitOfWork = unitOfWork;
        }

        public void RegisterUser(RegistrationUserModel registrationUser)
        {
            var salt = _cryptoContext.GenerateSaltAsBase64();
            var user = new UserEntity
            {
                Email = registrationUser.Email,
                Firstname = registrationUser.Firstname,
                Lastname = registrationUser.Lastname,
                Password = Convert.ToBase64String(_cryptoContext.DeriveKey(registrationUser.Password, salt)),
                Salt = salt,
            };
            _unitOfWork.Repository<UserEntity>().Insert(user);
            _unitOfWork.SaveChanges();
        }
    }
}