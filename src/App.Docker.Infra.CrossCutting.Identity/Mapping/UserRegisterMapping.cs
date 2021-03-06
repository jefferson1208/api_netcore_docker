using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Docker.Infra.CrossCutting.Identity.Entities;

namespace App.Docker.Infra.CrossCutting.Identity.Mapping
{
    public class UserRegisterMapping : IEntityTypeConfiguration<UserRegister>
    {
        public void Configure(EntityTypeBuilder<UserRegister> builder)
        {
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
                .HasColumnType("datetimeoffset");

            builder.Property(p => p.LockoutEnabled)
                .HasColumnType("bit");

            builder.Property(p => p.AccessFailedCount)
                .HasColumnType("int");

            builder.ToTable("Users");
        }
    }
}
