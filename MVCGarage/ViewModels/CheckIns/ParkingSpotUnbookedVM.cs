using MVCGarage.Models;
using System;

namespace MVCGarage.ViewModels.CheckIns
{
    public class ParkingSpotUnbookedVM
    {
        public CheckIn CheckIn { get; set; }
        public int NbMonths { get; set; }
    }
}