using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.CheckIns
{
    public class ParkingSpotHistoricVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public IEnumerable<CheckIn> CheckIns { get; set; }
    }
}