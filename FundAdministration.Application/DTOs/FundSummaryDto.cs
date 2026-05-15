using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.DTOs
{
    public class FundSummaryDto
    {
        public Guid FundId { get; set; }

        public decimal TotalSubscriptions { get; set; }

        public decimal TotalRedemptions { get; set; }

        // Bonus: number of investors for the fund
        public int NumberOfInvestors { get; set; }

        // Computed convenience property
        public decimal NetInvestment => TotalSubscriptions - TotalRedemptions;
    }
}
