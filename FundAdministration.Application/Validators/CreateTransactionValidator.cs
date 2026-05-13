using FluentValidation;
using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.InvestorId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0);
        }
    }
    
    
}
