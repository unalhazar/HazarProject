using Dapper;
using Hazar.DataAccessLayer.Abstract;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq.Expressions;

namespace Hazar.DataAccessLayer.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public IConfiguration Configuration { get; }

        public GenericRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private async Task<T> WithConnection<T>(Func<SQLiteConnection, Task<T>> execute)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                await dbConnection.OpenAsync();
                try
                {
                    return await Task.Run(() => execute(dbConnection));
                }
                finally
                {
                    if (dbConnection.State != ConnectionState.Closed)
                    {
                        dbConnection.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var whereClause = predicate != null ? $" WHERE {predicate}" : string.Empty;

            return await WithConnection(async connection =>
            {
                return await connection.QueryAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE ActiveState = 1 {whereClause} ORDER BY Id DESC");
            });
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                return await connection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {typeof(TEntity).Name} WHERE Id = @Id", new { Id = id });
            });
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await WithConnection(async connection =>
            {
                return await connection.ExecuteAsync($"UPDATE {typeof(TEntity).Name} SET ActiveState = 0 WHERE Id = @Id", new { Id = id });
            });
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            return await WithConnection(async connection =>
            {
                var propertyNames = string.Join(", ", entity.GetType().GetProperties().Where(p => p.Name != "Id").Select(p => p.Name));
                var parameterNames = string.Join(", ", entity.GetType().GetProperties().Where(p => p.Name != "Id").Select(p => "@" + p.Name));

                var query = $"INSERT INTO {typeof(TEntity).Name} ({propertyNames}) VALUES ({parameterNames}); SELECT last_insert_rowid();";

                var id = await connection.ExecuteScalarAsync<int>(query, entity);

                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null)
                    idProperty.SetValue(entity, id);

                return id;
            });
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await WithConnection(async connection =>
            {
                var propertyUpdates = entity.GetType().GetProperties()
                    .Where(p => p.Name != "Id" && p.Name != "CreatedTime")
                    .Select(p => $"{p.Name} = @{p.Name}");
                var query = $"UPDATE {typeof(TEntity).Name} SET {string.Join(", ", propertyUpdates)} WHERE Id = @Id";
                return await connection.ExecuteAsync(query, entity);
            });
        }
    }
}
