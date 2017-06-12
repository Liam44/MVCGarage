using MVCGarage.Models;
using MVCGarage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGarage.Controllers
{
    public class AvailableSpots
    {
        ParkingSpotsRepository parkingSpots = new ParkingSpotsRepository();
        CheckInsRepository checkIns = new CheckInsRepository();

        public IEnumerable<ParkingSpot> AvailableParkingSpots(ETypeVehicle vehicleType = ETypeVehicle.undefined)
        {
            return parkingSpots.ParkingSpots(vehicleType).Select(p => new
            {
                ParkingSpot = p,
                CheckIns = checkIns.CheckIns().Where(ch => !ch.Free && ch.ParkingSpotID == p.ID)
            })
            .Where(chp => chp.CheckIns.Count() == 0)
            .Select(chp => chp.ParkingSpot);
        }
    }
}