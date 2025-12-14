using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Project.Domain.Repositories.Base;
using Project.Host.Base.Configs;
using Project.Host.Base.Lazyloads;
using System.Collections.Concurrent;

namespace Project.Infrastructure.MongoDB
{
    public class MongoRepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private static MongoClient _client;
        private IMongoDatabase _database;
        protected ILazyloadProvider _lazyloadProvider;

        private ConcurrentDictionary<string, IMongoDatabase> _cache = [];


        // Cấu hình connection string (Nên lấy từ config file)
        private readonly string _connectionString;

        public MongoRepositoryBase(IOptions<ConnectionConfig> options, ILazyloadProvider lazyloadProvider)
        {
            _lazyloadProvider = lazyloadProvider;
            _connectionString = options.Value.MongoDB;
            var settings = MongoClientSettings.FromConnectionString(_connectionString);

            _client = new MongoClient(settings);
        }

        /// <summary>
        /// Chuyển qua db tương ứng
        /// </summary>
        /// <param name="databaseName"></param>
        public void GetDatabase(string databaseName)
        {
            _database = _cache.GetOrAdd(databaseName, dbName => _client.GetDatabase(dbName));
        }

        private IMongoCollection<T> Collection => _database.GetCollection<T>(typeof(T).Name);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = await Collection.Find(FilterDefinition<T>.Empty).ToListAsync();
            return list;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            //TODO: check lại prop nào có attribute key để build filter đúng
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(T entity)
        {
            // Giả định rằng entity có một thuộc tính Id kiểu Guid
            var idProperty = typeof(T).GetProperty("_id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property of type Guid");

            _ = Guid.TryParse(idProperty.GetValue(entity)?.ToString(), out Guid id);
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
                idProperty.SetValue(entity, id);
            }

            await Collection.InsertOneAsync(entity);
            return id;
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            // Giả định rằng entity có một thuộc tính Id kiểu Guid
            var idProperty = typeof(T).GetProperty("_id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property of type Guid");

            _ = Guid.TryParse(idProperty.GetValue(entity)?.ToString(), out Guid id);

            if (id == Guid.Empty)
                throw new InvalidOperationException("Entity Id cannot be empty");

            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await Collection.ReplaceOneAsync(filter, entity);

            return result.ModifiedCount > 0;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            //TODO: check lại prop nào có attribute key để build filter đúng
            throw new NotImplementedException();
        }
    }
}
