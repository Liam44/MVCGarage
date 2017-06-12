﻿using MVCGarage.Models;
using System;

namespace MVCGarage.ViewModels.CheckIns
{
    public class ParkingSpotUnbooked
    {
        public CheckIn CheckIn { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int NbMonths { get; set; }
        public double TotalAmount { get; set; }
    }
}