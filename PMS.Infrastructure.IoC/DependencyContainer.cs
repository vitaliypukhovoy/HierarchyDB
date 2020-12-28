using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Infrastructure.DataAccess.Context;
using PMS.Infrastructure.DataAccess.Export;
using PMS.Infrastructure.DataAccess.Model;
using PMS.Infrastructure.DataAccess.Repo;

namespace PMS.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<PMSContext>(
                    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));


            services.AddTransient<IExportToExcel, ExportToExcel>((_)=> new ExportToExcel());

            services.AddTransient<IRepository<Projects>, Repository<Projects>>((_) => new Repository<Projects>(Configuration, "Projects"));

            services.AddTransient<IRepository<Tasks>, Repository<Tasks>>((_) => new Repository<Tasks>(Configuration, "Tasks"));

            services.AddTransient<IRepository<MemoryReportTable>, Repository<MemoryReportTable>>((_) => new Repository<MemoryReportTable>(Configuration, "MemoryReportTable"));

            services.AddTransient<PMSContext>();
        }
    }
}

