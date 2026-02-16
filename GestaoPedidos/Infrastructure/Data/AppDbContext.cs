using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GestaoPedidos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

    }
}
