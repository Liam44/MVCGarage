using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.ParkingSpots
{
    public class CreateParkingSpotsVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public Dictionary<VehicleType, double> DefaultFees { get; set; }
        public string ErrorMessage { get; set; }
    }
}