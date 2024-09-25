using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RRBank.Domain.Database;

namespace RRBank.Infra.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

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
                .HasMaxLength(11);

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

            builder.Property(x => x.CelNumber)
                .IsRequired()
                .HasColumnName("CelNumber")
                .HasColumnType("VARCHAR")
                .HasMaxLength(14);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.Manager)
                .WithMany(x => x.Clients)
                .HasForeignKey(c => c.ManagerId)
                .HasConstraintName("FK_Manager_Clients")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
