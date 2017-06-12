using MVCGarage.Models;

namespace MVCGarage.ViewModels.Garage
{
    public class BookedParkingSpotVM
    {
        public Vehicle Vehicle { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}