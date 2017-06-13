using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Navigation property - Allows the 1..1 relation to the "DefaultFee" table
        [ForeignKey("DefaultFee")]
        [Display(Name = "Default Fee")]
        public int DefaultFeeID { get; set; }

        [Display(Name = "Default Fee")]
        public virtual DefaultFee DefaultFee { get; set; }
        // --- //
    }
}