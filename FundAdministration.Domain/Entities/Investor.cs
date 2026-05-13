using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Domain.Entities
{
    public class Investor
    {
        public Guid InvestorId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Guid FundId { get; set; }

        public Fund Fund { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        = new List<Transaction>();
    }
}
