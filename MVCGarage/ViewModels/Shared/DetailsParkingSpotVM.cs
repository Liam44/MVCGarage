﻿using MVCGarage.Models;

namespace MVCGarage.ViewModels.Shared
{
    public class DetailsParkingSpotVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public CheckIn CheckIn { get; set; }
        public string Availability { get; set; }
    }
}