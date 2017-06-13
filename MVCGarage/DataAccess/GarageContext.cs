using MVCGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCGarage.DataAccess
{
    public class GarageContext : DbContext
    {
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<DefaultFee> DefaultFees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public GarageContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One-To-Many
            modelBuilder.Entity<Vehicle>()
                        .HasRequired<VehicleType>(vt => vt.VehicleType)
                        .WithMany(v => v.Vehicles);

            modelBuilder.Entity<DefaultFee>()
                .HasRequired<VehicleType>(f => f.VehicleType)
                .WithRequiredDependent(vt => vt.DefaultFee);
        }
    }
}