using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Docker.Domain.Entities;

namespace App.Docker.Infra.Data.Mapping.Users
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FullName)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.RegistrationDate)
                .HasColumnType("datetime");

            builder.Property(p => p.UserName)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.NormalizedUserName)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Email)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.NormalizedEmail)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.EmailConfirmed)
                .HasColumnType("bit");

            builder.Property(p => p.PasswordHash)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.SecurityStamp)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.ConcurrencyStamp)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("varchar(150)");

            builder.Property(p => p.PhoneNumberConfirmed)
                .HasColumnType("bit");

            builder.Property(p => p.TwoFactorEnabled)
                .HasColumnType("bit");

            builder.Property(p => p.LockoutEnd)
                .HasColumnType("datetime");

            builder.Property(p => p.LockoutEnabled)
                .HasColumnType("bit");

            builder.Property(p => p.AccessFailedCount)
                .HasColumnType("int");
        }
    }
}
