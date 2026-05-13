using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using FundAdministration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Infrastructure.Repositories
{
    public class FundRepository
    : GenericRepository<Fund>, IFundRepository
    {
        public FundRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
