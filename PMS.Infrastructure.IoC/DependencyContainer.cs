using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure.DataAccess.Repo;
using PMS.Infrastructure.DataAccess.Context;
using PMS.Infrastructure.DataAccess.Services;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PMS.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

           
            ////services.AddDbContext<PMSContext>(options =>
            ////          options.UseSqlServer(
            ////              Configuration.GetConnectionString("DefaultConnection")));


            //services.AddDbContext<PMSContext>(
            //    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            ////Register dapper in scope    
            //services.AddScoped<IDapper, Dapperr>();
            ////Data
            //services.AddTransient<IProjectRepository, ProjectRepository>();
            //services.AddTransient<ITaskRepository, TaskRepository>();
            //services.AddTransient<PMSContext>();           
        }
    }
}

