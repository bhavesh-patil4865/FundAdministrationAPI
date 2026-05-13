using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.DTOs
{
    public class CreateFundDto
    {
        public string Name { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public DateTime LaunchDate { get; set; }
    }
}
