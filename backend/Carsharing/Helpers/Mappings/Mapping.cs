using AutoMapper;
using Carsharing.Forms;
using Carsharing.Infastructure.GoogleAPI;
using Entities.Model;

namespace Carsharing.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserResult, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.given_name))
                .ForMember(dest => dest.UserSurname, opt => opt.MapFrom(src => src.family_name));


            CreateMap<RegistrationDto, User>();

            //Отдать BirthDay в User, чтобы не пришлось мапить.
            CreateMap<GetUserResult, UserInfo>()
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.birthday));
        }
    }
}
