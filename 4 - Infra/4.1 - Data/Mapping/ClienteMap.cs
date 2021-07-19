using arroba.suino.webapi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace arroba.suino.webapi.infra.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(prop => prop.ApiKey);

            builder.Property(prop => prop.ApiKey)
                .IsRequired()
                .HasColumnName("ApiKey")
                .HasColumnType("varchar(36)");

            builder.Property(prop => prop.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("varchar(45)");

            builder.Property(prop => prop.ApiSecret)
                .IsRequired()
                .HasColumnName("ApiSecret")
                .HasColumnType("varchar(36)");

            builder.Property(prop => prop.Ativo)
                .IsRequired()
                .HasColumnName("Ativo")
                .HasColumnType("TINYINT");

            builder.Property(prop => prop.CodDesenvolvedor)
                .IsRequired()
                .HasColumnName("CodDesenvolvedor")
                .HasColumnType("varchar(36)");
        }
    }
}