using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGarage.Models
{
    public class DefaultFee
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        public double Fee { get; set; }

        // Navigation property - Allows the 1..1 relation to the "VehicleType" table
        [ForeignKey("VehicleType")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeID { get; set; }

        [Display(Name = "Vehicle Type")]
        public virtual VehicleType VehicleType { get; set; }
        // --- //

        // Navigation property - Allows the 1..* relation to the "ParkingSpot" table
        public virtual ICollection<ParkingSpot> ParkingSpots { get; set; }
        // --- //
    }
}