using AutoMapper;
using DotNet5.API.Data;
using DotNet5.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5.API.Configurations
{
    public class MapperInitilizer:Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Country, CreateHotelDTO>().ReverseMap();
        }
    }
}
