using Dapper;
using Microsoft.Extensions.Configuration;
using PMS.Infrastructure.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PMS.Infrastructure.DataAccess.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IConfiguration _config;
        private readonly string Connectionstring = "DefaultConnection";
        private readonly string _tableName;

        public Repository(IConfiguration config, string tableName)
        {
            _config = config;
            _tableName = tableName;
        }


        private SqlConnection SqlConnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }

        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            if (conn.State == ConnectionState.Closed)
                conn.OpenAsync();

            return conn;
        }


        // Get all Data Projects or Tasks 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }


        //Report Get
        public async Task<IEnumerable<T>> GetAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(insertQuery, parms, null, null, commandType);
            }
        }

        //Get Project or Task by Id
        public async Task<T> GetAsync(int id, string Id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE {Id}=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{"Projects"} with id [{id}] could not be found.");
                return result;
            }
        }


        //Remove Item from Project or Task
        public async Task RemoveAsync(int id, string Id)
        {

            using (var connection = CreateConnection())
            {
              _ =  await connection.ExecuteAsync($"DELETE {_tableName} WHERE {Id} = @Id", new { Id = id }, null, null, CommandType.Text);
            }
        }


        // Create Project and Task Post
        public async Task InsertAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, parms, null, null, commandType);
            }
        }


        //Update Project or Task
        public async Task UpdateAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using (var connection = CreateConnection())
            {
                using (var tran = connection.BeginTransaction())
                    try
                    {
                      _ =  await connection.ExecuteAsync(insertQuery, parms, tran, null, commandType);
                        tran.Commit();

                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
            }
        }
    }
}
