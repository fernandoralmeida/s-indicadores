using IDN.Core.Empresa.Models;
using IDN.Core.Geojson.Models;
using IDN.Core.Municipio.Models;
using IDN.Data.Config;
using IDN.Data.Config.Geojson;
using IDN.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IDN.Data.Context;

public class ContextApp : DbContext
{
    public ContextApp()
    {

    }
    public ContextApp(DbContextOptions<ContextApp> options) : base(options)
    {

    }

    public DbSet<MMunicipio>? Municipios { get; set; }
    public DbSet<MEmpresa>? Empresas { get; set; }

    #region Geojson
    public DbSet<MFeatures>? GeojsonFeatures { get; set; }
    public DbSet<MGeometry>? GeojsonGeometries { get; set; }
    public DbSet<MCoordinates>? GeojsonCoordinates { get; set; }
    public DbSet<MProperties>? GeojsonProperties { get; set; }
    #endregion


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(DataBase.ConnectionString);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MunicipioDBMap());
        modelBuilder.ApplyConfiguration(new EmpresaDBMap());
        modelBuilder.ApplyConfiguration(new MapFeatures());
        modelBuilder.ApplyConfiguration(new MapGeometry());
        modelBuilder.ApplyConfiguration(new MapProperties());
        modelBuilder.ApplyConfiguration(new MapCoordinates());
        base.OnModelCreating(modelBuilder);
    }
}