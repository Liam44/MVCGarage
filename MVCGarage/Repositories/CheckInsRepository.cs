﻿using MVCGarage.DataAccess;
using MVCGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCGarage.Repositories
{
    public class CheckInsRepository : IDisposable
    {
        private GarageContext db = new GarageContext();

        public IEnumerable<CheckIn> CheckIns()
        {
            return db.CheckIns;
        }

        public CheckIn CheckIn(int? id)
        {
            return db.CheckIns.Find(id);
        }

        /// <summary>
        /// Returns informations on the currently booked parking spot
        /// </summary>
        /// <param name="parkingSpotId">ID of the booked parking spot</param>
        /// <returns></returns>
        public CheckIn BookedCheckInByParkingSpot(int parkingSpotId)
        {
            return CheckIns().SingleOrDefault(ch => ch.Booked && ch.ParkingSpotID == parkingSpotId && !ch.Free);
        }

        /// <summary>
        /// Returns informations on the currently booked parking spot
        /// </summary>
        /// <param name="vehicleId">ID of the vehicle for which the parking spot has been booked</param>
        /// <returns></returns>
        public CheckIn BookedCheckInByVehicle(int vehicleId)
        {
            return CheckIns().SingleOrDefault(ch => ch.Booked && ch.VehicleID == vehicleId && !ch.Free);
        }

        /// <summary>
        /// Returns informations on the currently parked parking spot
        /// </summary>
        /// <param name="parkingSpotId">Parking spot currently occupied by a vehicle</param>
        /// <returns></returns>
        public CheckIn ParkedCheckInByParkingSpot(int checkInId)
        {
            return CheckIns().SingleOrDefault(ch => !ch.Booked && ch.ParkingSpotID == checkInId && !ch.Free);
        }

        /// <summary>
        /// Returns informations on the currently booked parking spot
        /// </summary>
        /// <param name="vehicleId">ID of the vehicle currently parked on the parking spot</param>
        /// <returns></returns>
        public CheckIn ParkedCheckInByVehicle(int vehicleId)
        {
            return CheckIns().SingleOrDefault(ch => !ch.Booked && ch.VehicleID == vehicleId && !ch.Free);
        }

        /// <summary>
        /// Returns historical informations about all the times the parking spot has been booked
        /// </summary>
        /// <param name="parkingSpotId">ID of the booked parking spot</param>
        /// <returns></returns>
        public IEnumerable<CheckIn> BookedCheckInsByParkingSpot(int parkingSpotId)
        {
            return CheckIns().Where(ch => ch.Booked && ch.ParkingSpotID == parkingSpotId);
        }

        /// <summary>
        /// Returns historical informations about all the times a parking spot has been booked for that vehicle
        /// </summary>
        /// <param name="vehicleId">ID of the vehicle which the parking spots have been booked for</param>
        /// <returns></returns>
        public IEnumerable<CheckIn> BookedCheckInsByVehicle(int vehicleId)
        {
            return CheckIns().Where(ch => ch.Booked && ch.VehicleID == vehicleId);
        }

        /// <summary>
        /// Returns historical informations about all the times the parking spot has been parked
        /// </summary>
        /// <param name="parkingSpotId">ID of the booked parking spot</param>
        /// <returns></returns>
        public IEnumerable<CheckIn> ParkedCheckInsByParkingSpot(int parkingSpotId)
        {
            return CheckIns().Where(ch => !ch.Booked && ch.ParkingSpotID == parkingSpotId);
        }

        /// <summary>
        /// Returns historical informations about all the times a parking spot has been parked on by that vehicle
        /// </summary>
        /// <param name="vehicleId">ID of the vehicle which occupied the parking spots</param>
        /// <returns></returns>
        public IEnumerable<CheckIn> ParkedCheckInsByVehicle(int vehicleId)
        {
            return CheckIns().Where(ch => !ch.Booked && ch.VehicleID == vehicleId);
        }

        public void Add(CheckIn checkIn)
        {
            db.CheckIns.Add(checkIn);
            SaveChanges();
        }

        public void Edit(CheckIn checkIn)
        {
            db.Entry(checkIn).State = EntityState.Modified;
            SaveChanges();
        }

        private void SaveChanges()
        {
            db.SaveChanges();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}