using FluentValidation;
using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Validators
{
    internal class CreateInvestorValidator : AbstractValidator<CreateInvestorDto>
    {
        public CreateInvestorValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.FundId)
                .NotEmpty();
        }
    
    }
}
