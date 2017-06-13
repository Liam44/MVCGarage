using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCGarage.Models
{
    public class VehicleType
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required] 
        public string Type { get; set; }

        // Navigation property - Allows the 1..* relation to the "ParkingSpot" table
        public virtual ICollection<ParkingSpot> ParkingSpots { get; set; }
        // --- //

        // Navigation property - Allows the 1..* relation to the "Vehicle" table
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        // --- //
    }
}