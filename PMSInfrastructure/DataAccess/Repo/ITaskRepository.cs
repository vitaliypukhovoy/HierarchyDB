using PMS.Infrastructure.DataAccess.Model;
using System.Collections.Generic;

namespace PMS.Infrastructure.DataAccess.Repo
{
    public interface ITaskRepository
    {
        void Create(Tasks user);
        void Delete(int id);
        Tasks Get(int id);
        List<Tasks> GetUsers();
        void Update(Tasks user);
    }
}
