using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Npgsql;
using TradingAppMvc.Application.Models.Repository;
using TradingAppMvc.Models.Repositories;

namespace TradingAppMvc.Repositories
{
   public class Repository<T> : IRepository<T>, IDisposable where T : class, IEntity
   {
      private readonly string _tableName;
      private readonly IDbConnection _sqlCon;

      public Repository(IDbConnection sqlCon)
      {
         DefaultTypeMap.MatchNamesWithUnderscores = true;
         _tableName = ToSnakeCase(typeof(T).Name);
         _sqlCon = sqlCon;
      }
      public virtual async Task<IEnumerable<T>> FindAll()
      {
         try
         {
            var sql = $"SELECT * FROM {_tableName}";
            return await _sqlCon.QueryAsync<T>(sql);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<T> FindById<Id>(Id id)
      {

         try
         {
            var sql = $"SELECT * FROM {_tableName} WHERE id = @id";
            return await _sqlCon.QueryFirstAsync<T>(
                sql,
                new { id });
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<IEnumerable<U>> FindByParameter<U>(QueryParameter queryParameter, IEnumerable<string> columns)
      {
         try
         {
            var columnData = new List<string>();
            foreach (var column in columns)
            {
               columnData.Add(ToSnakeCase(column));
            }

            var sql = $"SELECT {string.Join(", ", columnData)} FROM {_tableName}";
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();
            if (queryParameter.Conditions.Count > 0)
            {
               // Add WHERE clauses for each filter condition
               var i = 0;
               foreach (var condition in queryParameter.Conditions)
               {
                  whereClauses.Add($"{ToSnakeCase(condition.Column)} {condition.Operator} @p{i}");
                  parameters.Add($"p{i}", condition.Value);
                  i++;
               }
            }
            if (whereClauses.Any())
            {
               sql += $" WHERE {string.Join(" AND ", whereClauses)}";
            }
            if (queryParameter.OrderByColumn.Count > 0)
            {
               // Add ORDER BY clause for each OrderByColumn
               var orderByClauses = queryParameter.OrderByColumn.Select(x => $"{x}");
               sql += $" ORDER BY {string.Join(", ", orderByClauses)}";
            }
            if (queryParameter.Limit.HasValue)
            {
               sql += $" LIMIT {queryParameter.Limit}";
               // Add LIMIT and OFFSET clauses for paging
               if (queryParameter.Offset.HasValue)
               {
                  sql += $" OFFSET {queryParameter.Offset}";
               }
            }

            // Execute the query and return the results
            var result = await _sqlCon.QueryAsync<U>(sql, parameters);
            return result;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<IEnumerable<T>> FindByParameter(QueryParameter queryParameter)
      {
         try
         {
            // Build the SQL query
            var sql = ConvertSql($"SELECT * FROM {_tableName}");
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();
            if (queryParameter.Conditions.Count > 0)
            {
               // Add WHERE clauses for each filter condition
               var i = 0;
               foreach (var condition in queryParameter.Conditions)
               {
                  whereClauses.Add($"{ToSnakeCase(condition.Column)} {condition.Operator} @p{i}");
                  parameters.Add($"p{i}", condition.Value);
                  i++;
               }
            }
            if (whereClauses.Any())
            {
               sql += $" WHERE {string.Join(" AND ", whereClauses)}";
            }
            if (queryParameter.OrderByColumn.Count > 0)
            {
               // Add ORDER BY clause for each OrderByColumn
               var orderByClauses = queryParameter.OrderByColumn.Select(x => $"{x}");
               sql += $" ORDER BY {string.Join(", ", orderByClauses)}";
            }
            if (queryParameter.Limit.HasValue)
            {
               sql += $" LIMIT {queryParameter.Limit}";
               // Add LIMIT and OFFSET clauses for paging
               if (queryParameter.Offset.HasValue)
               {
                  sql += $" OFFSET {queryParameter.Offset}";
               }
            }

            // Execute the query and return the results
            return await _sqlCon.QueryAsync<T>(sql, parameters);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<T> AddEntity(T entity)
      {
         try
         {
            var p = new DynamicParameters();
            var properties = GetPropertyInfo();
            var columns = string.Join(",", properties.Where(x => x.Name != "Id").Select(x => ToSnakeCase(x.Name)));
            var values = string.Join(",", properties.Where(x => x.Name != "Id").Select(x => $"@{x.Name}"));

            foreach (var item in properties)
            {
               p.Add(item.Name, item.GetValue(entity));
            }

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values}) RETURNING *";

            return await _sqlCon.QueryFirstAsync<T>(sql, p);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<bool> UpdateEntity(T entity)
      {
         try
         {
            var properties = GetPropertyInfo();
            var columnUpdates = new List<string>();
            var updates = string.Empty;
            updates = string.Join(',', GetPropertyInfo().Select(c => $"{ToSnakeCase(c.Name)} = @{c.Name}"));
            var sql = $"UPDATE {_tableName} SET {updates} WHERE id = @id";

            return await _sqlCon.ExecuteAsync(sql, entity) > 0;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<bool> UpdateEntity(T entity, string[] columns)
      {
         try
         {
            if (columns.Count() == 0)
            {
               return false;
            }
            var properties = GetPropertyInfo();
            var columnUpdates = new List<string>();
            var updates = string.Empty;
            foreach (var column in columns)
            {
               columnUpdates.Add(ToSnakeCase(column));
            }
            updates = string.Join(',', columnUpdates.Select((c, i) => $"{c} = p@{i}"));
            var sql = $"UPDATE {_tableName} SET {updates} WHERE id = @id";

            return await _sqlCon.ExecuteAsync(sql, entity) > 0;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual async Task<bool> DeleteEntity(T entity)
      {
         try
         {
            var sql = $"DELETE FROM {_tableName} WHERE id = @id";

            return await _sqlCon.ExecuteAsync(sql, entity) > 0;
         }
         catch (Exception)
         {
            throw;
         }
      }

      private string ConvertSql(string sql)
      {
         // Get the properties of the class
         var properties = typeof(T).GetProperties().Where(p => !p.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)));

         // Build the list of columns
         var columns = new List<string>();
         foreach (var property in properties)
         {
            // Get the column name for the property
            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            var columnName = columnAttribute != null ? columnAttribute.Name : ToSnakeCase(property.Name);

            // Add the column to the list, with an optional alias
            columns.Add(columnName == property.Name ? columnName : $"{columnName} as {property.Name}");
         }

         // Replace the SELECT * with the list of columns
         return sql.Replace("*", string.Join(", ", columns));
      }

      private IEnumerable<PropertyInfo> GetPropertyInfo()
      {
         var test = typeof(T).GetProperties().Where(p => p.Name != "Id").Select(x => x.Name);
         return typeof(T).GetProperties().Where(p => p.Name != "Id" && !p.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute)));
      }

      private string ToSnakeCase(string input)
      {
         return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).ToLower();
      }

      public void Dispose()
      {
         _sqlCon.Dispose();
      }
   }
}
