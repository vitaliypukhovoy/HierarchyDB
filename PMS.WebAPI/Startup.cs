using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS.Infrastructure.DataAccess.Context;
using PMS.WebAPI.Services;
using PMS.WebAPI.Repo;
using PMS.WebAPI.Model;
using System.Collections;
using System.Collections.Generic;

namespace PMS.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddControllers();
            services.AddDbContext<PMSContext>(
               options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            //Register dapper in scope    
            //Register dapper in scope    
            services.AddScoped<IRepository<Projects>, Repository<Projects>>((_)=> new Repository<Projects>(Configuration, "Projects"));

            //services.AddScoped<IRepository<Tasks>, Repository<Tasks>>((_) => new Repository<Tasks>(Configuration, "Tasks"));

            services.AddScoped<IRepository<MemoryReportTable>, Repository<MemoryReportTable>>((_) => new Repository<MemoryReportTable>(Configuration, "MemoryReportTable"));
            //Data            
            services.AddTransient<PMSContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
