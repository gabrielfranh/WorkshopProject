using Microsoft.EntityFrameworkCore;
using OficinasAPI.Model;

namespace OficinasAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<Oficina> Oficinas { get; set; }
    }
}
