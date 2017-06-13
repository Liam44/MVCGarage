using MVCGarage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGarage.Controllers
{
    public class OwnersVehicles
    {
        OwnersRepository owners = new OwnersRepository();
        VehiclesRepository vehicles = new VehiclesRepository();

        public void AssignOwnerToVehicle(int OwnerID, int VehicleID)
        { 
            
        }
    }
}