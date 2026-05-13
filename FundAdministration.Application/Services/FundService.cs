using FundAdministration.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundAdministration.Application.DTOs;

using FundAdministration.Domain.Entities;
using FundAdministration.Domain.Interfaces;
using AutoMapper;

namespace FundAdministration.Application.Services
{
    public class FundService : IFundService
    {
        private readonly IFundRepository _repository;

        private readonly IMapper _mapper;

        public FundService(
            IFundRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FundDto>> GetAllAsync()
        {
            var funds = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<FundDto>>(funds);
        }

        public async Task<FundDto?> GetByIdAsync(Guid id)
        {
            var fund = await _repository.GetByIdAsync(id);

            return _mapper.Map<FundDto>(fund);
        }

        public async Task CreateAsync(CreateFundDto dto)
        {
            var fund = _mapper.Map<Fund>(dto);

            fund.FundId = Guid.NewGuid();

            await _repository.AddAsync(fund);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateFundDto dto)
        {
            var fund = await _repository.GetByIdAsync(id);

            if (fund == null)
                throw new Exception("Fund not found");

            fund.Name = dto.Name;
            fund.Currency = dto.Currency;
            fund.LaunchDate = dto.LaunchDate;

            _repository.Update(fund);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var fund = await _repository.GetByIdAsync(id);

            if (fund == null)
                throw new Exception("Fund not found");

            _repository.Delete(fund);

            await _repository.SaveChangesAsync();
        }

        public async Task<FundSummaryDto>
            GetFundSummaryAsync(Guid fundId)
        {
            var fund = await _repository.GetByIdAsync(fundId);

            if (fund == null)
                throw new Exception("Fund not found");

            return new FundSummaryDto
            {
                FundId = fundId
            };
        }


    }
}
