﻿// <auto-generated />
using System;
using FM_API.Persistance.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    [DbContext(typeof(FMContext))]
    [Migration("20220418020852_QuarterMigration")]
    partial class QuarterMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FM_API.Entities.Budget", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("presupuesto", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Estimate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<long>("Id_budget")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_income")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_spent")
                        .HasColumnType("bigint");

                    b.Property<int>("Plan")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id_budget");

                    b.ToTable("estimaciones", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Income", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<long?>("Id_income")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id_income");

                    b.ToTable("ingresos", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Rol", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Rol_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("rol", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Spent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<long?>("Id_spent")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id_spent");

                    b.ToTable("gastos", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long>("Id_budget")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_income")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_spent")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id_budget");

                    b.ToTable("transacciones", "public");
                });

            modelBuilder.Entity("FM_API.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Id_rol")
                        .HasColumnType("bigint");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id_rol");

                    b.ToTable("usuario", "public");
                });

            modelBuilder.Entity("FM_API.Entities.Estimate", b =>
                {
                    b.HasOne("FM_API.Entities.Budget", "Budget")
                        .WithMany("Estimates")
                        .HasForeignKey("Id_budget")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("FM_API.Entities.Income", b =>
                {
                    b.HasOne("FM_API.Entities.Estimate", null)
                        .WithMany("Income")
                        .HasForeignKey("Id_income");

                    b.HasOne("FM_API.Entities.Transaction", null)
                        .WithMany("Income")
                        .HasForeignKey("Id_income");
                });

            modelBuilder.Entity("FM_API.Entities.Spent", b =>
                {
                    b.HasOne("FM_API.Entities.Estimate", null)
                        .WithMany("Expenses")
                        .HasForeignKey("Id_spent");

                    b.HasOne("FM_API.Entities.Transaction", null)
                        .WithMany("Expenses")
                        .HasForeignKey("Id_spent");
                });

            modelBuilder.Entity("FM_API.Entities.Transaction", b =>
                {
                    b.HasOne("FM_API.Entities.Budget", "Budget")
                        .WithMany("Transactions")
                        .HasForeignKey("Id_budget")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("FM_API.Entities.User", b =>
                {
                    b.HasOne("FM_API.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("Id_rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("FM_API.Entities.Budget", b =>
                {
                    b.Navigation("Estimates");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FM_API.Entities.Estimate", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Income");
                });

            modelBuilder.Entity("FM_API.Entities.Transaction", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Income");
                });
#pragma warning restore 612, 618
        }
    }
}