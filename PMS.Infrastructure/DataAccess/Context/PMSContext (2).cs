using Microsoft.EntityFrameworkCore;

namespace PMS.Infrastructure.DataAccess.Context
{
    public class PMSContext : DbContext
    {
        public PMSContext() { }
        public PMSContext(DbContextOptions<PMSContext> options) : base(options) { }

    }
}