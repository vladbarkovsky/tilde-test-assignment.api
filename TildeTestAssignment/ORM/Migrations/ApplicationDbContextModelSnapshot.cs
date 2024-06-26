﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TildeTestAssignment.ORM.Services;

#nullable disable

namespace TildeTestAssignment.ORM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TildeTestAssignment.ORM.Entities.Debt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CreditorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DebtorId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Refunded")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CreditorId");

                    b.HasIndex("DebtorId");

                    b.ToTable("Debts");
                });

            modelBuilder.Entity("TildeTestAssignment.ORM.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("TildeTestAssignment.ORM.Entities.Debt", b =>
                {
                    b.HasOne("TildeTestAssignment.ORM.Entities.Person", "Creditor")
                        .WithMany("CreditorDebts")
                        .HasForeignKey("CreditorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("TildeTestAssignment.ORM.Entities.Person", "Debtor")
                        .WithMany("DebtorDebts")
                        .HasForeignKey("DebtorId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Creditor");

                    b.Navigation("Debtor");
                });

            modelBuilder.Entity("TildeTestAssignment.ORM.Entities.Person", b =>
                {
                    b.Navigation("CreditorDebts");

                    b.Navigation("DebtorDebts");
                });
#pragma warning restore 612, 618
        }
    }
}
