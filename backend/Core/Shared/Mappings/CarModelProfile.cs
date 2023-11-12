using AutoMapper;
using Contracts;
using Domain.Entities;

namespace Shared.Mappings;

public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        CreateMap<CarModel, CarModelDto>()
            .ForMember(dest=>dest.ImageUrl, 
                source => source
                    .MapFrom(carModel=> CarModelDto.GenerateImageUrl(carModel.ImageName)));
        
        CreateMap<CarModel, ExtendedCarModelDto>()
            .ForMember(dest=>dest.TariffName, 
                source => source
                    .MapFrom(carModel=> carModel.Tariff!.Name))
            .ForMember(dest=>dest.Restrictions, 
                source => source
                    .MapFrom(carModel=> carModel.Tariff!.MaxMileage))
            .ForMember(dest=>dest.Price, 
                source => source
                    .MapFrom(carModel=> carModel.Tariff!.Price))
            .ForMember(dest=>dest.ImageUrl, 
                source => source
                    .MapFrom(carModel=> CarModelDto.GenerateImageUrl(carModel.ImageName)));
        
        CreateMap<Car, FreeCarDto>()
            .ForMember(dest => dest.CarId,
                src => src
                    .MapFrom(car => car.Id))
            .ForMember(dest => dest.Location,
                src => src
                    .MapFrom(car =>new GeoPoint(car.ParkingLatitude, car.ParkingLongitude)))
            .ForMember(dest => dest.Plate,
                src => src
                    .MapFrom(car => car.LicensePlate));

        CreateMap<Car, CarDto>();
        
        CreateMap<CreateCarModelDto, CarModel>();
        
    }
}