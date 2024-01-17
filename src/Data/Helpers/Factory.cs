using IDN.Core.Empresa.Models;
using IDN.Data.Context;
using IDN.Data.Interface;

namespace IDN.Data.Helpers;

public static class Factory<T> where T : class
{
    public static IData<MEmpresa> NewDataEmpresa()
    {
       return new DataEmpresa();
    }

    public static IMongoDB<T> NewDataMongoDB()
    {
        return new MongoDB<T>();
    }

    public static IData<MEmpresa> NewPostgres()
    {
        return new DataNpgsql();
    }

}