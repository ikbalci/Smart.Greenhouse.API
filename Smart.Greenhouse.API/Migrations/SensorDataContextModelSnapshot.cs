﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Smart.Greenhouse.API.Infrastructure.Data;

#nullable disable

namespace Smart.Greenhouse.API.Migrations
{
    [DbContext(typeof(SensorDataContext))]
    partial class SensorDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Smart.Greenhouse.API.Core.Entities.SensorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AirPurifierOn")
                        .HasColumnType("bit");

                    b.Property<double>("AirQuality")
                        .HasColumnType("float");

                    b.Property<bool>("CoolerOn")
                        .HasColumnType("bit");

                    b.Property<double>("CoolingDemand")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HeaterOn")
                        .HasColumnType("bit");

                    b.Property<double>("HeatingDemand")
                        .HasColumnType("float");

                    b.Property<double>("Moisture")
                        .HasColumnType("float");

                    b.Property<string>("MoistureCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MoistureTemperatureRatio")
                        .HasColumnType("float");

                    b.Property<bool>("PumperOn")
                        .HasColumnType("bit");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.Property<string>("TemperatureCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.ToTable("SensorData");
                });
#pragma warning restore 612, 618
        }
    }
}
