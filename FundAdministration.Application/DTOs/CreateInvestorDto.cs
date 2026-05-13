using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.DTOs
{
    public class CreateInvestorDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid FundId { get; set; }
    }
}
