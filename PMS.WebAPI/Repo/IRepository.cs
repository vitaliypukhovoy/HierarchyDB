using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;


namespace PMS.WebAPI.Repo
{
    public interface IRepository<T>
    {       
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteRowAsync(int id);
        Task<T> GetAsync(int id);
        Task<int> SaveRangeAsync(IEnumerable<T> list);
        Task UpdateAsync(T t);        
        Task InsertAsync(string insertQuery, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
