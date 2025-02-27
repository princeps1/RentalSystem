﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalSystem.Data;

#nullable disable

namespace RentalSystem.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241212083837_V1")]
    partial class V1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KorisnikVozilo", b =>
                {
                    b.Property<int>("KorisniciID")
                        .HasColumnType("int");

                    b.Property<int>("VozilaID")
                        .HasColumnType("int");

                    b.HasKey("KorisniciID", "VozilaID");

                    b.HasIndex("VozilaID");

                    b.ToTable("KorisnikVozilo");
                });

            modelBuilder.Entity("WebTemplate.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrVozacke")
                        .HasColumnType("int");

                    b.Property<string>("ImePrezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("WebTemplate.Models.Vozilo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BrDanaIznajmljivanja")
                        .HasColumnType("int");

                    b.Property<int>("CenaVozila")
                        .HasColumnType("int");

                    b.Property<int>("Godiste")
                        .HasColumnType("int");

                    b.Property<bool>("Iznajmljen")
                        .HasColumnType("bit");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PredjenoKm")
                        .HasColumnType("int");

                    b.Property<string>("RegistarskiBroj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Vozila", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("WebTemplate.Models.Automobil", b =>
                {
                    b.HasBaseType("WebTemplate.Models.Vozilo");

                    b.Property<int>("BrSedista")
                        .HasColumnType("int");

                    b.Property<string>("Gorivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Karoserija")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Automobili", (string)null);
                });

            modelBuilder.Entity("WebTemplate.Models.Motor", b =>
                {
                    b.HasBaseType("WebTemplate.Models.Vozilo");

                    b.Property<string>("Vrsta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Motori", (string)null);
                });

            modelBuilder.Entity("KorisnikVozilo", b =>
                {
                    b.HasOne("WebTemplate.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("KorisniciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTemplate.Models.Vozilo", null)
                        .WithMany()
                        .HasForeignKey("VozilaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebTemplate.Models.Automobil", b =>
                {
                    b.HasOne("WebTemplate.Models.Vozilo", null)
                        .WithOne()
                        .HasForeignKey("WebTemplate.Models.Automobil", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebTemplate.Models.Motor", b =>
                {
                    b.HasOne("WebTemplate.Models.Vozilo", null)
                        .WithOne()
                        .HasForeignKey("WebTemplate.Models.Motor", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
