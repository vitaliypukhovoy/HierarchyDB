using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace PMS.Infrastructure.DataAccess.Repo
{
    public interface IRepository<T>
    {
       Task<IEnumerable<T>> GetAsync(string insertQuery, DynamicParameters parms, CommandType commandType);
        Task<IEnumerable<T>> GetAllAsync();
        Task RemoveAsync(int id, string Id);
        Task<T> GetAsync( int id, string Id);       
        Task UpdateAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.Text);        
        Task InsertAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
