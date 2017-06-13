using MVCGarage.DataAccess;
using MVCGarage.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCGarage.Controllers
{
    public class PopulateSelectLists
    {
        static GarageContext db = new GarageContext();

        public static List<SelectListItem> PopulateVehicleTypes(int? vehicleTypeId = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (VehicleType vehicleType in db.VehicleTypes)
                items.Add(new SelectListItem
                {
                    Value = vehicleType.ID.ToString(),
                    Text = vehicleType.Type,
                    Selected = vehicleTypeId == vehicleType.ID
                });

            return items;
        }

        public static List<SelectListItem> PopulateOwners(int? ownerId = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Owner owner in db.Owners)
                items.Add(new SelectListItem
                {
                    Value = owner.ID.ToString(),
                    Text = string.Format("{0} {1}", owner.Lname.ToUpper(), owner.Fname),
                    Selected = ownerId == owner.ID
                });

            return items;
        }
    }
}