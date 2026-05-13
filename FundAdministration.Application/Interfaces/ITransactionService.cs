using FundAdministration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Interfaces
{
    public interface ITransactionService
    {
        Task RegisterTransactionAsync(CreateTransactionDto dto);
    }
}
