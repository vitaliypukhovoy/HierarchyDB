using Microsoft.EntityFrameworkCore;

namespace PMS.Infrastructure.DataAccess.Context
{
    public class PMSContext : DbContext
    {
        public PMSContext() { }
        public PMSContext(DbContextOptions<PMSContext> options) : base(options) { }
   

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        //}
    }
}  