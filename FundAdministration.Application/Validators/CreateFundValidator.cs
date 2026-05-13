using FluentValidation;
using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Validators
{
    public class CreateFundValidator : AbstractValidator<CreateFundDto>
    {
        public CreateFundValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.LaunchDate)
                .NotEmpty();
        }
    

    }
}
