using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGarage.Models
{
    public class Vehicle
    {
        [Key]
        [Display(Name = "Vehicle ID")]
        public int ID { get; set; }

        [Display(Name = "Owner")]
        public virtual Owner Owner { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }

        [Display(Name = "Regitration plate")]
        public string RegistrationPlate { get; set; }

        // Navigation property - Allows the 1..1 relation to the "VehicleType" table
        [ForeignKey("VehicleType")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeID { get; set; }

        [Display(Name = "Vehicle Type")]
        public virtual VehicleType VehicleType { get; set; }
        // --- //

        // Navigation property - Allows the 1..* relation to the "CheckIn" table
        public virtual ICollection<CheckIn> CheckIns { get; set; }
        // --- //
    }
}