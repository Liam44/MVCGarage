using MVCGarage.Models;
using MVCGarage.Repositories;
using MVCGarage.ViewModels.CheckIns;
using MVCGarage.ViewModels.ParkingSpots;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVCGarage.Controllers
{
    public class CheckInsController : Controller
    {
        private CheckInsRepository db = new CheckInsRepository();

        private IEnumerable<CheckIn> Sort(IEnumerable<CheckIn> list, string sortOrder)
        {
            ViewBag.CheckInIDSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "id_asc";

            ViewBag.ParkingSpotLabelSortParam = sortOrder == "label_asc" ? "label_desc" : "label_asc";

            ViewBag.VehicleRegNumberSortParam = sortOrder == "regnum_asc" ? "regnum_desc" : "regnum_asc";
            ViewBag.VehicleOwnerSortParam = sortOrder == "owner_asc" ? "owner_desc" : "owner_asc";

            ViewBag.CheckInTimeSortParam = sortOrder == "checkintime_asc" ? "checkintime_desc" : "checkintime_asc";
            ViewBag.CheckOutTimeSortParam = sortOrder == "checkouttime_asc" ? "checkouttime_desc" : "checkouttime_asc";
            ViewBag.TotalAmountSortParam = sortOrder == "totalamount_asc" ? "totalamount_desc" : "totalamount_asc";
            ViewBag.StatusSortParam = sortOrder == "status_asc" ? "status_desc" : "status_asc";

            switch (sortOrder)
            {
                case "id_asc":
                    list = list.OrderBy(ch => ch.ID);
                    break;
                case "label_asc":
                    list = list.OrderBy(ch => ch.ParkingSpot.Label);
                    break;
                case "label_desc":
                    list = list.OrderByDescending(ch => ch.ParkingSpot.Label);
                    break;
                case "regnum_asc":
                    list = list.OrderBy(ch => ch.Vehicle.RegistrationPlate);
                    break;
                case "regnum_desc":
                    list = list.OrderByDescending(ch => ch.Vehicle.RegistrationPlate);
                    break;
                case "owner_asc":
                    list = list.OrderBy(ch => ch.Vehicle.Owner);
                    break;
                case "owner_desc":
                    list = list.OrderByDescending(ch => ch.Vehicle.Owner);
                    break;
                case "checkintime_asc":
                    list = list.OrderBy(ch => ch.CheckInTime);
                    break;
                case "checkintime_desc":
                    list = list.OrderByDescending(ch => ch.CheckInTime);
                    break;
                case "checkouttime_asc":
                    list = list.OrderBy(ch => ch.CheckOutTime);
                    break;
                case "checkouttime_desc":
                    list = list.OrderByDescending(ch => ch.CheckOutTime);
                    break;
                case "totalamount_asc":
                    list = list.OrderBy(ch => ch.TotalAmount);
                    break;
                case "totalamount_desc":
                    list = list.OrderByDescending(ch => ch.TotalAmount);
                    break;
                case "status_asc":
                    list = list.OrderBy(ch => ch.Status());
                    break;
                case "status_desc":
                    list = list.OrderBy(ch => ch.Status());
                    break;
                default:
                    list = list.OrderByDescending(ch => ch.ID);
                    break;
            }

            return list;
        }

        [HttpGet]
        public ActionResult VehicleHistoric(int? vehicleId, string sortOrder)
        {
            if (vehicleId == null)
                return RedirectToAction("Index", "Garage");

            return View(new VehicleHistoricVM
            {
                Vehicle = new GarageController().Vehicle(vehicleId),
                CheckIns = Sort(db.CheckInsByVehicle((int)vehicleId), sortOrder).ToList()
            });
        }

        [HttpGet]
        public ActionResult ParkingSpotHistoric(int? parkingSpotId, string sortOrder)
        {
            if (parkingSpotId == null)
                return RedirectToAction("Index", "Garage");

            return View(new ParkingSpotHistoricVM
            {
                ParkingSpot = new GarageController().ParkingSpot(parkingSpotId),
                CheckIns = Sort(db.CheckInsByParkingSpot((int)parkingSpotId), sortOrder).ToList()
            });
        }

        [HttpGet]
        public ActionResult VehicleCheckedIn(SelectAParkingSpotVM viewModel)
        {
            // Check that the vehicle isn't already parked/hasn't booked a place
            if (db.CheckInByVehicle(viewModel.VehicleID) != null)
                return RedirectToAction("Index", "Garage");

            Vehicle vehicle = new GarageController().Vehicle(viewModel.VehicleID);

            if (vehicle == null)
                return RedirectToAction("BookAParkingSpot",
                                        "Vehicles",
                                        new
                                        {
                                            checkIn = viewModel.CheckIn,
                                            errorMessage = "You must select a vehicle!"
                                        });

            // Check in the vehicle ID to the parking spot
            ParkingSpot parkingSpot = new GarageController().ParkingSpot(viewModel.ParkingSpotID);

            if (parkingSpot == null)
                return RedirectToAction("CheckInAVehicle", new
                {
                    vehicleId = viewModel.VehicleID,
                    errorMessage = "You must select a parking spot!"
                });

            CheckIn checkIn = db.CheckIn(viewModel.VehicleID, viewModel.ParkingSpotID);
            checkIn.Vehicle = vehicle;
            checkIn.ParkingSpot = parkingSpot;

            // Displays the chosen parking spot
            return View(checkIn);
        }

        [HttpGet]
        public ActionResult VehicleCheckedOut(int? vehicleId)
        {
            if (vehicleId == null)
                return RedirectToAction("Index", "Garage");

            CheckIn checkIn = new CheckInsVehicles().CheckInByVehicle((int)vehicleId);

            if (checkIn == null)
                return RedirectToAction("Index", "Garage");

            // Check out the vehicle ID to the parking spot
            DateTime now = DateTime.Now;
            int nbMinutes = (int)Math.Truncate((now - (DateTime)checkIn.CheckInTime).TotalMinutes) + 1;
            double totalAmount = nbMinutes * checkIn.ParkingSpot.VehicleType.DefaultFee.Fee;

            checkIn = db.CheckOut(checkIn.ID, now, totalAmount);

            // Displays the bill
            return View(new VehicleCheckedOutVM
            {
                CheckIn = checkIn,
                NbMinutes = nbMinutes
            });
        }

        [HttpGet]
        public ActionResult ParkingSpotBooked(SelectAParkingSpotVM viewModel)
        {
            // Check that the vehicle isn't already parked/hasn't booked a place
            if (db.CheckInByVehicle(viewModel.VehicleID) != null)
                return RedirectToAction("Index", "Garage");

            // Check in the vehicle ID to the parking spot
            CheckIn checkIn = db.Book(viewModel.VehicleID, viewModel.ParkingSpotID);
            checkIn.Vehicle = new GarageController().Vehicle(viewModel.VehicleID);
            checkIn.ParkingSpot = new GarageController().ParkingSpot(viewModel.ParkingSpotID);

            // Displays the chosen parking spot
            return View(checkIn);
        }

        [HttpGet]
        public ActionResult ParkingSpotUnbooked(int? vehicleId)
        {
            if (vehicleId == null)
                return RedirectToAction("Index", "Garage");

            CheckIn checkIn = new CheckInsVehicles().CheckInByVehicle((int)vehicleId);

            if (checkIn == null)
                return RedirectToAction("Index", "Garage");

            // Check out the vehicle ID to the parking spot
            DateTime now = DateTime.Now;
            int nbMonths = (int)Math.Truncate((now - (DateTime)checkIn.CheckInTime).TotalDays / 30) + 1;
            double totalAmount = nbMonths * checkIn.ParkingSpot.MonthlyFee();

            checkIn = db.CheckOut(checkIn.ID, now, totalAmount);

            // Displays the bill
            return View(new ParkingSpotUnbookedVM
            {
                CheckIn = checkIn,
                NbMonths = nbMonths
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
