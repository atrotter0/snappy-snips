using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
    public class HairSalonContext : DbContext
    {
        public DbSet<Stylist> Stylists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<StylistSpecialty> StylistsSpecialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"User ID=" + System.Environment.GetEnvironmentVariable("DATABASE_USER") +
                                        ";Password=" + System.Environment.GetEnvironmentVariable("DATABASE_PASSWORD") +
                                        ";Host=" + System.Environment.GetEnvironmentVariable("DATABASE_HOST") +
                                        ";Port=" + System.Environment.GetEnvironmentVariable("DATABASE_PORT") +
                                        ";Database=" + System.Environment.GetEnvironmentVariable("DATABASE_NAME") +
                                        ";Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;"
                                    );
        }
    }
}
