﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingGarageManagement.cs.Models;

namespace ParkingGarageManagement.cs.Migrations
{
    [DbContext(typeof(GarageContext))]
    partial class GarageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckIn");

                    b.Property<int>("LotPosition");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.LotRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxRange");

                    b.Property<int>("MinRange");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("LotRanges");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("PersonTz");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("PersonTz")
                        .IsUnique()
                        .HasFilter("[PersonTz] IS NOT NULL");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cost");

                    b.Property<int>("MaxHeight");

                    b.Property<int>("MaxLength");

                    b.Property<int>("MaxWidth");

                    b.Property<string>("Name");

                    b.Property<int>("TicketTypeId");

                    b.Property<int>("TimeLimit");

                    b.HasKey("Id");

                    b.HasIndex("TicketTypeId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.TicketClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.ToTable("TicketClasses");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.TicketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId");

                    b.Property<int>("TicketId");

                    b.Property<int>("VehicleHeight");

                    b.Property<int>("VehicleLength");

                    b.Property<int>("VehicleTypeId");

                    b.Property<int>("VehicleWidth");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Lot", b =>
                {
                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.LotRange", b =>
                {
                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Ticket", b =>
                {
                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.TicketType", "TicketType")
                        .WithMany()
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.Vehicle", b =>
                {
                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParkingGarageManagement.cs.Models.Domain.VehicleType", b =>
                {
                    b.HasOne("ParkingGarageManagement.cs.Models.Domain.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
