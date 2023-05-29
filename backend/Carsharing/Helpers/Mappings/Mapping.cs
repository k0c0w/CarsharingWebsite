﻿using AutoMapper;
using Carsharing.Forms;
using Carsharing.Hubs.ChatEntities;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels.Admin;
using Carsharing.ViewModels.Admin.Car;
using Carsharing.ViewModels.Admin.User;
using Contracts;
using Contracts.Tariff;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
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

            CreateMap<OpenChatsVM, UserConnection>().ReverseMap();

            CreateMap<TariffVM, AdminTariffDto>().ReverseMap();
        }


        private static Contracts.File IFormFileToStream(IFormFile formFile)
        {
            Contracts.File file;

            var _stream = formFile.OpenReadStream();

            //_stream.Seek(0, SeekOrigin.Begin);

            file = new Contracts.File()
            {
                Name = formFile.FileName,
                Content = _stream
            };


            return file;

            _stream.Dispose();
        }
    }
}
