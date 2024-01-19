﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motorcycle.Infra.Data;

#nullable disable

namespace Motorcycle.Infra.Data.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20240119182949_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Motorcycle.Domain.BudgetAggregate.BudgetDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Component")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdMotorcycle")
                        .HasColumnType("int");

                    b.Property<int>("IdUsers")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdMotorcycle");

                    b.HasIndex("IdUsers");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("Motorcycle.Domain.MaintenanceAggregate.MaintenanceDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("IdMoto")
                        .HasColumnType("int");

                    b.Property<int>("IdOwner")
                        .HasColumnType("int");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdMoto");

                    b.HasIndex("IdOwner");

                    b.ToTable("Maintenance");
                });

            modelBuilder.Entity("Motorcycle.Domain.MotorcycleAggregate.MotorcycleDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdOwner")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdOwner");

                    b.ToTable("Motorcycle");
                });

            modelBuilder.Entity("Motorcycle.Domain.UserAggregate.UsersDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Motorcycle.Domain.BudgetAggregate.BudgetDomain", b =>
                {
                    b.HasOne("Motorcycle.Domain.MotorcycleAggregate.MotorcycleDomain", "Motorcycle")
                        .WithMany("Budget")
                        .HasForeignKey("IdMotorcycle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Motorcycle.Domain.UserAggregate.UsersDomain", "User")
                        .WithMany("Budget")
                        .HasForeignKey("IdUsers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorcycle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Motorcycle.Domain.MaintenanceAggregate.MaintenanceDomain", b =>
                {
                    b.HasOne("Motorcycle.Domain.MotorcycleAggregate.MotorcycleDomain", "Motorcycle")
                        .WithMany("Maintenances")
                        .HasForeignKey("IdMoto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Motorcycle.Domain.UserAggregate.UsersDomain", "User")
                        .WithMany("Maintenance")
                        .HasForeignKey("IdOwner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorcycle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Motorcycle.Domain.MotorcycleAggregate.MotorcycleDomain", b =>
                {
                    b.HasOne("Motorcycle.Domain.UserAggregate.UsersDomain", "User")
                        .WithMany("Motorcycles")
                        .HasForeignKey("IdOwner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Motorcycle.Domain.MotorcycleAggregate.MotorcycleDomain", b =>
                {
                    b.Navigation("Budget");

                    b.Navigation("Maintenances");
                });

            modelBuilder.Entity("Motorcycle.Domain.UserAggregate.UsersDomain", b =>
                {
                    b.Navigation("Budget");

                    b.Navigation("Maintenance");

                    b.Navigation("Motorcycles");
                });
#pragma warning restore 612, 618
        }
    }
}
