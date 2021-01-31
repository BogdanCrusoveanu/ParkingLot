using Microsoft.EntityFrameworkCore;
using ParkingApp.Model;

namespace ParkingApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleSpot> VehicleSpots { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VehicleSpot>(vehicleSpot =>
            {
                vehicleSpot.HasOne(vs => vs.Vehicle)
                    .WithMany(v => v.VehicleSpots)
                    .HasForeignKey(vs => vs.VehicleId)
                    .IsRequired();

                vehicleSpot.HasOne(vs => vs.ParkingSpot)
                    .WithMany(s => s.VehicleSpots)
                    .HasForeignKey(vs => vs.ParkingSpotId)
                    .IsRequired();
            });
        }
    }
}

