using Microsoft.Extensions.DependencyInjection;

using IDN.Core;
using IDN.Core.Empresa.Interfaces;
using IDN.Core.Empresa.Services;
using IDN.Core.Empresa.Models;

using IDN.Data.Repositories;
using IDN.Services.Empresa.Interfaces;
using IDN.Services.Empresa.Services;
using IDN.Services.Base;
using IDN.Data.Context;
using IDN.Core.Municipio.Models;
using IDN.Services.Municipio.Services;
using IDN.Services.Municipio.Interfaces;
using IDN.Core.Municipio.Services;
using IDN.Core.Municipio.Interfaces;
using IDN.Services.AutoMapper;
using IDN.Services.Geojson.Interfaces;
using IDN.Services.Geojson.Services;
using IDN.Core.Geojson.Models;
using IDN.Core.Geojson.Interfaces;

namespace IDN.Coupler;

public static class Services
{
    public static void Register(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddScoped<DataNpgsql>();
        services.AddScoped<ContextApp>();

        services.AddScoped<IServiceBase<MEmpresa>, ServiceBase<MEmpresa>>();
        services.AddScoped<IServiceEmpresa, ServiceEmpresa>();

        services.AddScoped<IServiceCore<MEmpresa>, ServiceCore<MEmpresa>>();
        services.AddScoped<IServiceCoreEmpresa, ServiceCoreEmpresa>();

        services.AddScoped<IRepositoryCore<MEmpresa>, RepositoryContext<MEmpresa>>();
        services.AddScoped<IRepositoryCoreEmpresa, RepositoryEmpresa>();

        services.AddScoped<IServiceBase<MMunicipio>, ServiceBase<MMunicipio>>();
        services.AddScoped<IServiceMunicipio, ServiceMunicipio>();

        services.AddScoped<IServiceCore<MMunicipio>, ServiceCore<MMunicipio>>();
        services.AddScoped<IServiceCoreMunicipio, ServiceCoreMunicipio>();

        services.AddScoped<IRepositoryCore<MMunicipio>, RepositoryContext<MMunicipio>>();
        services.AddScoped<IRepositoryCoreMunicipio, RepositoryMunicipio>();

        services.AddScoped<IServiceBase<MGeojson>, ServiceBase<MGeojson>>();
        services.AddScoped<IServiceGeojson, ServiceGeojson>();

        services.AddScoped<IServiceCore<MGeojson>, ServiceCore<MGeojson>>();
        services.AddScoped<IServiceCoreGeojson, ServiceCoreGeojson>();

        services.AddScoped<IRepositoryCore<MGeojson>, RepositoryContext<MGeojson>>();
        services.AddScoped<IRepositoryCoreGeojson, RepositoryGeojson>();

        services.AddAutoMapper(typeof(MapperProfile));
        services.AddMemoryCache();

    }
}