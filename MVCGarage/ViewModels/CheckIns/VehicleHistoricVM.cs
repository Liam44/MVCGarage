using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.CheckIns
{
    public class VehicleHistoricVM
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<CheckIn> CheckIns { get; set; }
    }
}