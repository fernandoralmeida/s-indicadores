using AutoMapper;
using IDN.Core.Cnae.Models;
using IDN.Core.Municipio.Models;
using IDN.Services.Cnae.View;
using IDN.Services.Municipio.View;

namespace IDN.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<VMunicipio, MMunicipio>().ReverseMap();
        CreateMap<VCnae, MCnae>().ReverseMap();
    }
}