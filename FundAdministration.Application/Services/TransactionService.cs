using AutoMapper;
using FundAdministration.Application.DTOs;
using FundAdministration.Application.Interfaces;
using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task RegisterTransactionAsync(
            CreateTransactionDto dto)
        {
            if (dto.Amount <= 0)
                throw new Exception(
                    "Transaction amount must be positive");

            var transaction =
                _mapper.Map<Transaction>(dto);

            transaction.TransactionId = Guid.NewGuid();

            transaction.TransactionDate = DateTime.UtcNow;

            await _repository.AddAsync(transaction);

            await _repository.SaveChangesAsync();
        }
    }
}
