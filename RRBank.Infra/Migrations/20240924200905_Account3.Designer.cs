﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RRBank.Infra;

#nullable disable

namespace RRBank.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240924200905_Account3")]
    partial class Account3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RRBank.Domain.Database.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("RRBank.Domain.Database.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasMaxLength(256)
                        .HasColumnType("INT")
                        .HasColumnName("Age");

                    b.Property<string>("CelNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("CelNumber");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Document");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("BIT")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("LastName");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<DateTime>("Register")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Register");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("RRBank.Domain.Database.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasMaxLength(256)
                        .HasColumnType("INT")
                        .HasColumnName("Age");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Document");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("BIT")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<DateTime>("Register")
                        .HasColumnType("DATETIME")
                        .HasColumnName("Register");

                    b.HasKey("Id");

                    b.ToTable("Manager", (string)null);
                });

            modelBuilder.Entity("RRBank.Domain.Database.Account", b =>
                {
                    b.HasOne("RRBank.Domain.Database.Client", "Client")
                        .WithMany("Accounts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RRBank.Domain.Database.Client", b =>
                {
                    b.HasOne("RRBank.Domain.Database.Manager", "Manager")
                        .WithMany("Clients")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Manager_Clients");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("RRBank.Domain.Database.Client", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("RRBank.Domain.Database.Manager", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
