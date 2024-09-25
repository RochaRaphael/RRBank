using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RRBank.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Infra.Mapping
{
    public class ManagerMap : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("Manager");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Document)
                .IsRequired()
                .HasColumnName("Document")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Age)
                .IsRequired()
                .HasColumnName("Age")
                .HasColumnType("INT")
                .HasMaxLength(256);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Register)
                .IsRequired()
                .HasColumnName("Register")
                .HasColumnType("DATETIME");

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("BIT");

        }
    }
}
