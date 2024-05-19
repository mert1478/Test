using D1TechTestCase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Validations
{
    public class LogOutAnotherModelValidator: AbstractValidator<LogOutAnotherModel>
    {
        public LogOutAnotherModelValidator()
        {
            RuleFor(x => x.Token).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SessionId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
