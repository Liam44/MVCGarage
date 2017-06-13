using MVCGarage.DataAccess;
using MVCGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCGarage.Repositories
{
    public class DefaultFeesRepository : IDisposable
    {
        private GarageContext db = new GarageContext();

        public IEnumerable<DefaultFee> DefaultFees()
        {
            return db.DefaultFees;
        }

        public DefaultFee DefaultFee(int? id)
        {
            return db.DefaultFees.Find(id);
        }

        public DefaultFee DefaultFeeByVehicleTypeID(int? vehicleTypeId)
        {
            return DefaultFees().SingleOrDefault(df => df.VehicleTypeID == vehicleTypeId);
        }

        public void Add(DefaultFee defaultFee)
        {
            db.DefaultFees.Add(defaultFee);
            SaveChanges();
        }

        public void Edit(DefaultFee defaultFee)
        {
            db.Entry(defaultFee).State = EntityState.Modified;
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