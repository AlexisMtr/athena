using Microsoft.EntityFrameworkCore;
using Athena.Models;
using System;

namespace Athena.Configuration
{
    public class AthenaContext : DbContext
    {
        public AthenaContext(DbContextOptions<AthenaContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceConfiguration>()
                .Property(e => e.ConfigurationUpdateCheckDelay)
                .HasConversion(e => e.Ticks, e => TimeSpan.FromTicks(e));

            modelBuilder.Entity<DeviceConfiguration>()
                .Property(e => e.PublicationDelay)
                .HasConversion(e => e.Ticks, e => TimeSpan.FromTicks(e));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Telemetry> Telemetries { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Pool> Pools { get; set; }
        public DbSet<DeviceConfiguration> DeviceConfiguration { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
