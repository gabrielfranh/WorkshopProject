using Microsoft.EntityFrameworkCore;
using AgendamentoAPI.Model;

namespace AgendamentoAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}
