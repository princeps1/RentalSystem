﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalSystem.Data;

#nullable disable

namespace RentalSystem.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RentalSystem.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrVozacke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImePrezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Korisnici", (string)null);
                });

            modelBuilder.Entity("RentalSystem.Models.Vozilo", b =>
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

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

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

                    b.HasIndex("KorisnikID");

                    b.ToTable("Vozila", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("RentalSystem.Models.Automobil", b =>
                {
                    b.HasBaseType("RentalSystem.Models.Vozilo");

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

            modelBuilder.Entity("RentalSystem.Models.Motor", b =>
                {
                    b.HasBaseType("RentalSystem.Models.Vozilo");

                    b.Property<string>("Vrsta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Motori", (string)null);
                });

            modelBuilder.Entity("RentalSystem.Models.Vozilo", b =>
                {
                    b.HasOne("RentalSystem.Models.Korisnik", "Korisnik")
                        .WithMany("Vozila")
                        .HasForeignKey("KorisnikID");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("RentalSystem.Models.Automobil", b =>
                {
                    b.HasOne("RentalSystem.Models.Vozilo", null)
                        .WithOne()
                        .HasForeignKey("RentalSystem.Models.Automobil", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalSystem.Models.Motor", b =>
                {
                    b.HasOne("RentalSystem.Models.Vozilo", null)
                        .WithOne()
                        .HasForeignKey("RentalSystem.Models.Motor", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalSystem.Models.Korisnik", b =>
                {
                    b.Navigation("Vozila");
                });
#pragma warning restore 612, 618
        }
    }
}
