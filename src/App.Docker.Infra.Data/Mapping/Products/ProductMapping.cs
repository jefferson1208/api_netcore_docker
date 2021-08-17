using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Docker.Domain.Entities;

namespace App.Docker.Infra.Data.Mapping.Products
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Description)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Price)
                .HasColumnType("money");

        }
    }
}
