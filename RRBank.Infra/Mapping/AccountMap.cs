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
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(x => x.Balance)
                .IsRequired()
                .HasColumnName("Balance")
                .HasColumnType("DECIMAL(18, 2)");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME");

            builder.Property(x => x.UpdateAt)
                .IsRequired()
                .HasColumnName("UpdateAt")
                .HasColumnType("DATETIME");

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Accounts)
                .HasForeignKey(c => c.ClientId)
                .HasConstraintName("FK_Client_Accounts")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
