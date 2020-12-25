using System.Collections.Generic;
using PMS.Infrastructure.DataAccess.Model;

namespace PMS.Infrastructure.DataAccess.Repo
{
    public interface IProjectRepository
    {
        void Create(Projects user);
        void Delete(int id);
        Projects Get(int id);
        List<Projects> GetUsers();
        void Update(Projects user);
    }
}
