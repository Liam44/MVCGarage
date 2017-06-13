using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.ParkingSpots
{
    public class CreateParkingSpotsVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public IEnumerable<DefaultFee> DefaultFees { get; set; }
        public string ErrorMessage { get; set; }
    }
}