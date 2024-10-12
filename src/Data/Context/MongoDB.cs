using IDN.Data.Helpers;
using IDN.Data.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IDN.Data.Context;

public class MongoDB<T> : IMongoDB<T> where T : class
{
    private static readonly IMongoClient _client = new MongoClient(DataBase.MongoDBServer);
    private readonly IMongoDatabase _database;
    public MongoDB()
    {
        if (_client == null)
        {
            throw new InvalidOperationException("MongoClient não foi inicializado corretamente.");
        }

        _database = _client.GetDatabase(DataBase.DBName);
    }

    public async Task<IEnumerable<T>> DoListAsync(FilterDefinition<T>? filter = null)
    {
        var collection = _database.GetCollection<T>(MongoDB<T>.GetCollectionName());

        var _filter = filter ?? new BsonDocument();

        var empresas = await collection.FindAsync(_filter);
        return await empresas.ToListAsync();
    }

    public async Task<int> InsertManyAsync(IEnumerable<T> list)
    {
        var collection = _database.GetCollection<T>(MongoDB<T>.GetCollectionName());
        await collection.InsertManyAsync(list);
        return list.Count();
    }

    private static string GetCollectionName()
    {
        // retorna o nome da coleção da interface IMongoDB<T>.
        return typeof(T).Name;
    }
}