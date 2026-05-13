using FundAdministration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.DTOs
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }

        public Guid InvestorId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
