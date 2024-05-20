using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CafeDuCoin.Models;


namespace CafeDuCoin.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Rental, RentalDto>().ReverseMap();
        }
    }
}