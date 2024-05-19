using D1TechTestCase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Validations
{
    public class FavoritesIdsModelValidator : AbstractValidator<FavoritesIdsModel>
    {
        public FavoritesIdsModelValidator()
        {
            RuleFor(x => x.Ids).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required")
                .Must(BeUnique).WithMessage("Guid list must contain unique values.");
        }
        private bool BeUnique(List<Guid> guidList)
        {
            if (guidList == null)
            {
                return true;
            }

            return guidList.Distinct().Count() == guidList.Count;
        }
    }
}
