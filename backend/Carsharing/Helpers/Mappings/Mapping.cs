using AutoMapper;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels.Admin;
using Carsharing.ViewModels.Admin.Car;
using Contracts;
using Contracts.Tariff;
using Domain.Entities;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            //Отдать BirthDay в User, чтобы не пришлось мапить.
            CreateMap<GetUserResult, UserInfo>()
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.birthday));



            //Services
            CreateMap<CreateCarModelVM, CreateCarModelDto>()
                .ForMember(dest => dest.ModelPhoto, opt => opt.MapFrom(src => IFormFileToStream(src.Image)));

            CreateMap<CarModelDto, CarModel>()
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => "models/" + src.ImageUrl))
                .ReverseMap();

            CreateMap<Tariff, AdminTariffDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TariffId))
                .ForMember(dest => dest.PriceInRubles, opt => opt.MapFrom(src => src.Price));

            CreateMap<TariffVM, AdminTariffDto>().ReverseMap();
        }


        private static Contracts.File IFormFileToStream(IFormFile formFile)
        {
            using Stream stream = new MemoryStream();

            formFile.CopyTo(stream);


            var file = new Contracts.File()
            {
                Name = formFile.FileName,
                Content = stream
            };
            return file;

        }
    }
}
