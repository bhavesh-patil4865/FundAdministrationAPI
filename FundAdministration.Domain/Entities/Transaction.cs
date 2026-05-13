using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public Guid InvestorId { get; set; }

        public Investor Investor { get; set; } = default!;

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
