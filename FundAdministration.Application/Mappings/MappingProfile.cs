using AutoMapper;
using FundAdministration.Application.DTOs;
using FundAdministration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace FundAdministration.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Fund, FundDto>();

            CreateMap<CreateFundDto, Fund>();

            CreateMap<Investor, InvestorDto>();

            CreateMap<CreateInvestorDto, Investor>();

            CreateMap<Transaction, TransactionDto>();

            CreateMap<CreateTransactionDto, Transaction>();
        }
    }
}
