using Microsoft.EntityFrameworkCore;
using CrudFortesCore.Models;
using CrudFortesCore.Repository.Abstract;

namespace CrudFortesCore.Data
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedor");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
            modelBuilder.Entity<Produto>().ToTable("Produto");
        }
    }
}
