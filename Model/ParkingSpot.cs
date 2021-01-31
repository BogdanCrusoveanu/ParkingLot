using System.Collections.Generic;

namespace ParkingApp.Model
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }
        public int ParkingSpotNumber { get; set; }
        public ICollection<VehicleSpot> VehicleSpots { get; set; }
    }
}
