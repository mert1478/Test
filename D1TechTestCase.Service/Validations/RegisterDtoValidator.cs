using D1TechTestCase.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Validations
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required")
                .Matches(@"^\S*$").WithMessage("New password must not contain any spaces.");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("New password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("New password must contain at least one special character.")
                .Matches(@"^\S*$").WithMessage("New password must not contain any spaces.");
        }
    }
}
