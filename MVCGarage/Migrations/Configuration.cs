namespace MVCGarage.Migrations
{
    using MVCGarage.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCGarage.DataAccess.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCGarage.DataAccess.GarageContext context)
        {
            VehicleType car = new VehicleType { Type = "Car" };
            VehicleType motorcycle = new VehicleType { Type = "Motorcycle" };
            VehicleType truck = new VehicleType { Type = "Truck" };
            VehicleType bus = new VehicleType { Type = "Bus" };

            context.VehicleTypes.AddOrUpdate(vt => vt.ID,
                car,
                motorcycle,
                truck,
                bus);

            Owner Mike = new Owner { Fname = "Mike", Lname = "Daughtrey", Gender = "M", LicenseNumber = "ABC-123-DEF" };
            Owner Wilhelm = new Owner { Fname = "Wilhelm", Lname = "Hansson", Gender = "M", LicenseNumber = "ABC-124-DEF" };
            Owner Liam = new Owner { Fname = "Liam", Lname = "Nottoosure", Gender = "M", LicenseNumber = "ABC-125-DEF" };

            context.Owners.AddOrUpdate(o => o.ID,
                Mike,
                Wilhelm,
                Liam);

            context.ParkingSpots.AddOrUpdate(p => p.ID,
                new ParkingSpot { Label = "101", VehicleType = car, Fee = 0.20 },
                new ParkingSpot { Label = "102", VehicleType = car, Fee = 0.20 },
                new ParkingSpot { Label = "103", VehicleType = motorcycle, Fee = 0.50 },
                new ParkingSpot { Label = "104", VehicleType = motorcycle, Fee = 0.50 },
                new ParkingSpot { Label = "201", VehicleType = truck, Fee = 0.80 },
                new ParkingSpot { Label = "202", VehicleType = bus, Fee = 1.00 });

            context.Vehicles.AddOrUpdate(v => v.ID,
                new Vehicle { RegistrationPlate = "ABC123", VehicleType = car, Owner = Mike },
                new Vehicle { RegistrationPlate = "ABC124", VehicleType = car, Owner = Wilhelm },
                new Vehicle { RegistrationPlate = "ABC125", VehicleType = motorcycle, Owner = Liam },
                new Vehicle { RegistrationPlate = "ABC126", VehicleType = motorcycle, Owner = Mike },
                new Vehicle { RegistrationPlate = "ABC127", VehicleType = car, Owner = Wilhelm },
                new Vehicle { RegistrationPlate = "ABC128", VehicleType = truck, Owner = Liam },
                new Vehicle { RegistrationPlate = "ABC129", VehicleType = bus, Owner = Mike });



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
