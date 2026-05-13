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
    public class InvestorService: IInvestorService
    {
        private readonly IInvestorRepository _repository;

        private readonly IMapper _mapper;

        public InvestorService(
            IInvestorRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvestorDto>> GetAllAsync()
        {
            var investors = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<InvestorDto>>(investors);
        }

        public async Task<InvestorDto?> GetByIdAsync(Guid id)
        {
            var investor = await _repository.GetByIdAsync(id);

            return _mapper.Map<InvestorDto>(investor);
        }

        public async Task CreateAsync(CreateInvestorDto dto)
        {
            var investor = _mapper.Map<Investor>(dto);

            investor.InvestorId = Guid.NewGuid();

            await _repository.AddAsync(investor);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var investor = await _repository.GetByIdAsync(id);

            if (investor == null)
                throw new Exception("Investor not found");

            _repository.Delete(investor);

            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionDto>>
            GetInvestorTransactionsAsync(Guid investorId)
        {
            var transactions =
                await _repository.GetInvestorTransactionsAsync(investorId);

            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

    }
}
