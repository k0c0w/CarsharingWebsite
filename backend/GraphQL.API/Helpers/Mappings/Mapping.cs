using AutoMapper;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin;
using Carsharing.ViewModels.Admin.Car;
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

            //Services
            CreateMap<AdminTariffDto, TariffVM>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MaxMileage, opt => opt.MapFrom(src => src.MaxMileage))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.PriceInRubles));

            CreateMap<CarModelDto, CarModel>()
                .ReverseMap();

            CreateMap<Tariff, AdminTariffDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TariffId))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.PricePerMinute));

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
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.UserInfo.Verified))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


            // Controllers
            CreateMap<CarDto, AdminCarVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsOpened, opt => opt.MapFrom(src => src.IsOpened))
                .ForMember(dest => dest.IsTaken, opt => opt.MapFrom(src => src.IsTaken))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.ParkingLatitude, opt => opt.MapFrom(src => src.ParkingLatitude))
                .ForMember(dest => dest.ParkingLongitude, opt => opt.MapFrom(src => src.ParkingLongitude))
                .ForMember(dest => dest.CarModelId, opt => opt.MapFrom(src => src.CarModelId))
                .ForMember(dest => dest.HasToBeNonActive, opt => opt.MapFrom(src => src.HasToBeNonActive));

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
