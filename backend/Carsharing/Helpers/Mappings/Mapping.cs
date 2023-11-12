using AutoMapper;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin;
using Carsharing.ViewModels.Admin.UserInfo;
using Contracts;
using Contracts.Tariff;
using Contracts.User;
using Domain.Entities;
using EditUserDto = Contracts.UserInfo.EditUserDto;

namespace Carsharing.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserResult, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.given_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.family_name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.email));

            //TODO: Отдать BirthDay в User, чтобы не пришлось мапить.
            CreateMap<GetUserResult, UserInfo>()
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.birthday));



            //Services
            CreateMap<CarModelDto, CarModel>()
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => "models/" + src.ImageUrl))
                .ReverseMap();

            CreateMap<Tariff, AdminTariffDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TariffId))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.Price));

            CreateMap<TariffVM, AdminTariffDto>().ReverseMap();

            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.UserInfo.Balance))
                .ForMember(dest => dest.Passport,
                    opt => opt.MapFrom(src => $"{src.UserInfo.PassportType}{src.UserInfo.Passport}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInfo.BirthDay))
                .ForMember(dest => dest.DriverLicense, opt => opt.MapFrom(src => src.UserInfo.DriverLicense))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            
            // Controllers
            CreateMap<UserInfo, UserVM>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.User.EmailConfirmed))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => new UserInfoVM
                {
                    Balance = src.Balance,
                    Passport = src.PassportType != null ? $"{src.PassportType} {src.Passport}" : null,
                    Verified = src.Verified,
                    BirthDay = DateOnly.FromDateTime(src.BirthDay),
                    DriverLicense = src.DriverLicense
                }));

            CreateMap<EditUserVM, EditUserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname));


            CreateMap<EditUserVm, EditUserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => src.Passport!.Substring(4)))
                .ForMember(dest => dest.PassportType, opt => opt.MapFrom(src => src.Passport!.Substring(4)))
                .ForMember(dest => dest.DriverLicense, opt => opt.MapFrom(src => src.DriverLicense));
        }
    }
}
