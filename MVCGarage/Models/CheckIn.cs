using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MVCGarage.Models
{
    public class CheckIn
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Check in time")]
        public DateTime CheckInTime { get; set; }

        [Required]
        public bool Booked { get; set; }

        [Display(Name = "Check out time")]
        public DateTime? CheckOutTime { get; set; }

        [Display(Name = "Total Amount")]
        public double? TotalAmount { get; set; }

        // Navigation property - Allows the 1..1 relation to the "ParkingSpot" table
        [ForeignKey("ParkingSpot")]
        public int ParkingSpotID { get; set; }

        public virtual ParkingSpot ParkingSpot { get; set; }
        // --- //

        // Navigation property - Allows the 1..1 relation to the "Vehicle" table
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        // --- //

        public string Status()
        {
            string status = string.Empty;

            if (CheckOutTime == null)
            {
                status = "Currently ";
            }

            if (Booked)
            {
                status += "Booked";
            }
            else
            {
                status += "Parked";
            }

            // Creates a TextInfo based on the "sv-SE" culture.
            return new CultureInfo("sv-SE", false).TextInfo.ToTitleCase(status);
        }

    }
}