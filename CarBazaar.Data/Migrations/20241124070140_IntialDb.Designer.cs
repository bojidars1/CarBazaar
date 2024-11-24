﻿// <auto-generated />
using System;
using CarBazaar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    [DbContext(typeof(CarBazaarDbContext))]
    [Migration("20241124070140_IntialDb")]
    partial class IntialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarBazaar.Data.Models.CarListing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Car Listing Identifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car Brand");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car Color");

                    b.Property<string>("ExtraInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Extra Car Info");

                    b.Property<string>("Gearbox")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car Gearbox");

                    b.Property<int>("Horsepower")
                        .HasColumnType("int")
                        .HasComment("Car Horsepower");

                    b.Property<long>("Km")
                        .HasColumnType("bigint")
                        .HasComment("Car KM");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car Listing Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Car Price");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2")
                        .HasComment("Car Production Date");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2")
                        .HasComment("Listing Publication Date");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car State");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Car Type");

                    b.HasKey("Id");

                    b.ToTable("CarListings");
                });
#pragma warning restore 612, 618
        }
    }
}
