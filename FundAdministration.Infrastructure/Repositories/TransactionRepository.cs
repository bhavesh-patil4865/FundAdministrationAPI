using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using FundAdministration.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context)
            : base(context)
        {
        }
    
    }
}
