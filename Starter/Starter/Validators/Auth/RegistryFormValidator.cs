using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Starter.Services.UserManagement.Models;

namespace Starter.API.Validators.Auth
{
    public class RegistryFormValidator : AbstractValidator<RegistrationUserModel>
    {
        public RegistryFormValidator()
        {
            RuleFor(x => x.Password)
                .Length(6, 24)
                .WithMessage("Invalid password length");
        }
    }
}