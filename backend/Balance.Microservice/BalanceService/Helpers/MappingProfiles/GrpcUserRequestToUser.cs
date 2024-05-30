using AutoMapper;
using BalanceMicroservice.Clients;
using BalanceService.Domain;
using BalanceService.Domain.ValueObjects;

namespace BalanceService.Helpers.MappingProfiles;

public class GrpcUserRequestToUser : Profile 
{
    public GrpcUserRequestToUser()
    {
        CreateMap<GrpcUserRequest, User>()
            .ForMember(dest => dest.Id, src => src
                .MapFrom(x => new UserId(x.Id)));
        
        CreateMap<User, GrpcUserRequest>()
            .ForMember(dest => dest.Id, src => src
                .MapFrom(x => x.Id.Value.ToString()));
    }
}