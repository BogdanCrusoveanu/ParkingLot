namespace ParkingApp.Model
{
    public class VehicleSpot
    {
        public VehicleSpot()
        {
        }

        public VehicleSpot(int vehicleId, int parkingSpotId)
        {
            VehicleId = vehicleId;
            ParkingSpotId = parkingSpotId;
        }

        public int VehicleSpotId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ParkingSpotId { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}
