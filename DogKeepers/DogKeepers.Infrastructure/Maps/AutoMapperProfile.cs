using AutoMapper;
using DogKeepers.Core.Entities;
using DogKeepers.Shared.DTOs;

namespace DogKeepers.Infrastructure.Maps
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Dog, DogDto>()
                .ForMember(d => d.PictureFile, s => s.MapFrom(src => src.Picture.PictureFile))
                .ReverseMap();

            CreateMap<Jwt, JwtDto>().ReverseMap();
            CreateMap<Race, RaceDto>().ReverseMap();
            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Administrator, AdministratorDto>().ReverseMap();
        }

    }
}
