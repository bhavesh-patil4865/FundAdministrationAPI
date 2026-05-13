using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Domain.Entities
{
    public class Fund
    {
        public Guid FundId { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public DateTime LaunchDate { get; set; }

        public ICollection<Investor> Investors { get; set; }
        = new List<Investor>();
    }
}
