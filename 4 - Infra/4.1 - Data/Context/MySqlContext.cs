using arroba.suino.webapi.Domain.Entities;
using arroba.suino.webapi.infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace arroba.suino.webapi.infra.Context
{
    public class MySqlContext: DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(new UsuarioMap().Configure);
        }
    }
}