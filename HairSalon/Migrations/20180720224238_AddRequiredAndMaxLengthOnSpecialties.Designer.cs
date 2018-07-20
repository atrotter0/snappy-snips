using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HairSalon.Models;

namespace HairSalon.Migrations
{
    [DbContext(typeof(HairSalonContext))]
    [Migration("20180720224238_AddRequiredAndMaxLengthOnSpecialties")]
    partial class AddRequiredAndMaxLengthOnSpecialties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("HairSalon.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<int>("StylistId");

                    b.HasKey("ClientId");

                    b.HasIndex("StylistId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HairSalon.Models.Specialty", b =>
                {
                    b.Property<int>("SpecialtyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("SpecialtyId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("HairSalon.Models.Stylist", b =>
                {
                    b.Property<int>("StylistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("StylistId");

                    b.ToTable("Stylists");
                });

            modelBuilder.Entity("HairSalon.Models.StylistSpecialty", b =>
                {
                    b.Property<int>("StylistSpecialtyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SpecialtyId");

                    b.Property<int>("StylistId");

                    b.HasKey("StylistSpecialtyId");

                    b.HasIndex("SpecialtyId");

                    b.HasIndex("StylistId");

                    b.ToTable("StylistsSpecialities");
                });

            modelBuilder.Entity("HairSalon.Models.Client", b =>
                {
                    b.HasOne("HairSalon.Models.Stylist", "Stylist")
                        .WithMany("Clients")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HairSalon.Models.StylistSpecialty", b =>
                {
                    b.HasOne("HairSalon.Models.Specialty", "Specialty")
                        .WithMany("StylistsSpecialties")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HairSalon.Models.Stylist", "Stylist")
                        .WithMany("StylistsSpecialties")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
