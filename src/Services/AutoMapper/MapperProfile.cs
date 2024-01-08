using AutoMapper;
using IDN.Core.Municipio.Models;
using IDN.Services.Municipio.View;

namespace IDN.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<VMunicipio, MMunicipio>().ReverseMap();
    }
}