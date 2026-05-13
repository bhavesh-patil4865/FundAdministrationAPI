using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.DTOs
{
    public class FundReportDto
    {
        public string FundName { get; set; } = string.Empty;

        public decimal NetInvestment { get; set; }

        public int InvestorCount { get; set; }
    }
}
