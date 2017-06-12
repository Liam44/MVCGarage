using MVCGarage.Models;

namespace MVCGarage.ViewModels.ParkingSpots
{
    public class EditParkingSpotVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public Vehicle ParkedVehicle { get; set; }
    }
}