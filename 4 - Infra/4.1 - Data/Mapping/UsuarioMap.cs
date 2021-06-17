using arroba.suino.webapi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arroba.suino.webapi.infra.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Id)
                .IsRequired()
                .HasColumnName("codUsuario")
                .HasColumnType("varchar(36)");

            builder.Property(prop => prop.Nome)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("varchar(45)");

            builder.Property(prop => prop.Ativo)
                .IsRequired()
                .HasColumnName("Ativo")
                .HasColumnType("TINYINT");
        }
    }
}