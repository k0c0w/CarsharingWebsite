using AutoMapper;
using BalanceMicroservice.Clients;
using BalanceService.Domain.ValueObjects;
using Transaction = BalanceService.Domain.Transaction;

namespace BalanceService.Helpers.MappingProfiles;

public class BalanceChangeRequestToTransaction : Profile
{
    public BalanceChangeRequestToTransaction()
    {
        CreateMap<BalanceChangeRequest, Transaction>()
            .ForMember(dest => dest.IntegerPart, src => src
                .MapFrom(x=>x.BalanceChange.IntegerPart))
            .ForMember(dest => dest.FractionPart, src => src
                .MapFrom(x=>x.BalanceChange.FractionPart))
            .ForMember(dest => dest.IsPositive, src => src
                .MapFrom(x=>x.BalanceChange.IsPositive));
        
    }
}