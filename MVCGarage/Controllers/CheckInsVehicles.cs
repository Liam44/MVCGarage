using MVCGarage.Models;
using MVCGarage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGarage.Controllers
{
    public class CheckInsVehicles
    {
        VehiclesRepository vehicles = new VehiclesRepository();
        CheckInsRepository checkIns = new CheckInsRepository();

        public CheckIn CheckInByVehicle(int vehicleId)
        {
            return checkIns.CheckInByVehicle(vehicleId);
        }

        public IEnumerable<Vehicle> ParkedVehicles()
        {
            return vehicles.Vehicles()
                .Select(v => new
                {
                    Vehicle = v,
                    CheckIn = checkIns.CheckIns().Where(ch => !ch.Booked && !ch.Free && ch.VehicleID == v.ID)
                })
                .Select(v_ch => v_ch.Vehicle);
        }

        public IEnumerable<Vehicle> UnparkedVehicles()
        {
            return vehicles.Vehicles()
                .Select(v => new
                {
                    Vehicle = v,
                    CheckIns = checkIns.CheckIns()
                                .Where(ch => !ch.Free && ch.VehicleID == v.ID).Select(ch=>ch.VehicleID)
                }).Where(vc => !vc.CheckIns.ToList().Contains(vc.Vehicle.ID)).Select(vc => vc.Vehicle);
        }

        public IEnumerable<InnerJoinResult> InnerJoin(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(v => new
            {
                Vehicle = v,
                CheckIn = checkIns.CheckIns().FirstOrDefault(ch => !ch.Free && ch.VehicleID == v.ID)
            }).Select(vch => new InnerJoinResult
            {
                Vehicle = vch.Vehicle,
                CheckIn = vch.CheckIn,
                ParkingSpot = vch.CheckIn == null ? null : vch.CheckIn.ParkingSpot
            });
        }
    }
}