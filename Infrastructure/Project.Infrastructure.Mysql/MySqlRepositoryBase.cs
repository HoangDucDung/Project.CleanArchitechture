using Dapper;
using Project.Domain.Repositories.Base;
using Project.Extensions.Extensions;
using Project.Host.Base.Lazyloads;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using static Dapper.SqlMapper;

namespace Project.Infrastructure.Mysql
{
    internal class MySqlRepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public ILazyloadProvider _lazyloadProvider { get; }

        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction? _transaction;

        private PropertyInfo propKey = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)))!;

        public MySqlRepositoryBase(ILazyloadProvider lazyloadProvider, IDbConnection connection, IDbTransaction? transaction)
        {
            _lazyloadProvider = lazyloadProvider;
            _connection = connection;
            _transaction = transaction;
        }

        protected string TableName
        {
            get
            {
                return typeof(T).GetPropertyTableName();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}";
            return await _connection.QueryAsync<T>(sql, transaction: _transaction);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            //TODO: lẫy theo attribute key để builf động Id trong query
            var sql = $"SELECT * FROM {TableName} WHERE {propKey} = @id";
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { id }, _transaction);
        }

        public virtual async Task<Guid> InsertAsync(T entity)
        {
            var (keyName, value) = GetKeyEntity(entity);
            var parameters = entity.ToDynamicParameters(); // Tiện ích bên dưới
            var sql = GenerateInsertQuery(entity);
            await _connection.ExecuteAsync(sql, parameters, _transaction);
            return value;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var parameters = entity.ToDynamicParameters();
            var sql = GenerateUpdateQuery(entity);
            var rows = await _connection.ExecuteAsync(sql, parameters, _transaction);
            return rows > 0;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            //TODO: lẫy theo attribute key để builf động Id trong query
            var sql = $"DELETE FROM {TableName} WHERE {propKey} = @id";
            var rows = await _connection.ExecuteAsync(sql, new { id }, _transaction);
            return rows > 0;
        }

        #region 🔧 Helper (Sinh câu SQL động)
        /// <summary>
        /// Get hoặc sinh key cho entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private (string, Guid) GetKeyEntity(T entity)
        {
            var keyProperty = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            
            if (keyProperty == null)
                throw new Exception("Không tìm thấy thuộc tính có [Key] trên entity");

            Guid id = (Guid)(keyProperty.GetValue(entity) ?? Guid.Empty);
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
                keyProperty.SetValue(entity, id);
            }
            return (keyProperty.Name, id);
        }

        /// <summary>
        /// BUild câu Insert động
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        private string GenerateInsertQuery(T entity)
        {
            var props = entity.GetType().GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => p.Name);

            var columns = string.Join(", ", props);
            var values = string.Join(", ", props.Select(p => "@" + p));
            return $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() as int)";
        }

        /// <summary>
        /// Build câu Update động
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GenerateUpdateQuery(T entity)
        {
            var props = entity.GetType().GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => $"{p.Name} = @{p.Name}");
            var setClause = string.Join(", ", props);
            return $"UPDATE {TableName} SET {setClause} WHERE Id = @Id";
        }
        #endregion
    }
}
