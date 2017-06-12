using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCGarage.Models
{
    public enum ETypeVehicle
    {
        [Description("All vehicle types")]
        undefined,
        [Description("Car")]
        car,
        [Description("Motorcycle")]
        motorcycle,
        [Description("Bus")]
        bus,
        [Description("Truck")]
        truck
    }
}