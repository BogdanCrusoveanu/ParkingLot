using System.Collections.Generic;

namespace ParkingApp.Model
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public ICollection<VehicleSpot> VehicleSpots { get; set; }
    }
}
