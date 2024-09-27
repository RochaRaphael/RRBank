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
    public class RequestCancellationMap : IEntityTypeConfiguration<RequestCancellation>
    {
        public void Configure(EntityTypeBuilder<RequestCancellation> builder)
        {
            builder.ToTable("RequestCancellation");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR");

            builder.Property(x => x.Document)
                .IsRequired()
                .HasColumnName("Document")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(x => x.ProcessedDate)
                .IsRequired()
                .HasColumnName("ProcessedDate")
                .HasColumnType("DATETIME");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("INT");

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.RequestCancellation)
                .HasForeignKey(c => c.ClientId)
                .HasConstraintName("FK_Client_RequestCancellation");
        }
    }
}
