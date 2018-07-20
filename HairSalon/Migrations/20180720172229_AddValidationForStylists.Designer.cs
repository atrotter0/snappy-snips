using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HairSalon.Models;

namespace HairSalon.Migrations
{
    [DbContext(typeof(HairSalonContext))]
    [Migration("20180720172229_AddValidationForStylists")]
    partial class AddValidationForStylists
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("HairSalon.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
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
        }
    }
}
