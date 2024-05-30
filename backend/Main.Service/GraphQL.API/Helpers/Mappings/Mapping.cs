using AutoMapper;
using Carsharing.Persistence.GoogleAPI;
using Contracts;
using Contracts.Tariff;
using Contracts.User;
using Domain.Entities;
using GraphQL.API.ViewModels;
using EditUserDto = Contracts.UserInfo.EditUserDto;

namespace Carsharing.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tariff, TariffDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TariffId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MaxMileage, opt => opt.MapFrom(src => src.MaxMileage))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.PricePerMinute))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.MaxBookMinutes, opt => opt.MapFrom(src => src.MaxAllowedMinutes))
                .ForMember(dest => dest.MinBookMinutes, opt => opt.MapFrom(src => src.MinAllowedMinutes));

            CreateMap<GetUserResult, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.given_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.family_name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.email));

            //TODO: Отдать BirthDay в User, чтобы не пришлось мапить.
            CreateMap<GetUserResult, UserInfo>()
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.birthday));

            CreateMap<CarModelDto, CarModel>()
                .ReverseMap();

            CreateMap<Tariff, AdminTariffDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TariffId))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.PricePerMinute));

            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.UserInfo.Balance))
                .ForMember(dest => dest.Passport,
                    opt => opt.MapFrom(src => $"{src.UserInfo.PassportType}{src.UserInfo.Passport}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserInfo.BirthDay))
                .ForMember(dest => dest.DriverLicense, opt => opt.MapFrom(src => src.UserInfo.DriverLicense))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.UserInfo.Verified))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            CreateMap<EditUserVm, EditUserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => src.Passport == null && src.Passport!.Length == 10 ? src.Passport!.Substring(4, 6): ""))
                .ForMember(dest => dest.PassportType, opt => opt.MapFrom(src => src.Passport == null && src.Passport!.Length == 10 ? src.Passport!.Substring(4) : ""))
                .ForMember(dest => dest.DriverLicense, opt => opt.MapFrom(src => src.DriverLicense));

            CreateMap<BookingVM, RentCarDto>()
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId));

            // 
            CreateMap<CarModel, CarModelDto>();

            CreateMap<CarModel, ExtendedCarModelDto>()
                .ForMember(dest => dest.TariffName,
                    source => source
                        .MapFrom(carModel => carModel.Tariff!.Name))
                .ForMember(dest => dest.Restrictions,
                    source => source
                        .MapFrom(carModel => carModel.Tariff!.MaxMileage))
                .ForMember(dest => dest.Price,
                    source => source
                        .MapFrom(carModel => carModel.Tariff!.PricePerMinute));

            CreateMap<Car, FreeCarDto>()
                .ForMember(dest => dest.CarId,
                    src => src
                        .MapFrom(car => car.Id))
                .ForMember(dest => dest.Location,
                    src => src
                        .MapFrom(car => new GeoPoint(car.ParkingLatitude, car.ParkingLongitude)))
                .ForMember(dest => dest.Plate,
                    src => src
                        .MapFrom(car => car.LicensePlate));

            CreateMap<Car, CarDto>();

            CreateMap<CreateCarModelDto, CarModel>();

        }
    }
}
