using MVCGarage.Models;
using System;

namespace MVCGarage.ViewModels.CheckIns
{
    public class VehicleCheckedOutVM
    {
        public CheckIn CheckIn { get; set; }
        public int NbMinutes { get; set; }
    }
}