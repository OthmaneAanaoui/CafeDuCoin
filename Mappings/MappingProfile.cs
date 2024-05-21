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
            CreateMap<Rental, RentalDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            
        }
    }
}