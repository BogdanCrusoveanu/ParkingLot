using ParkingApp.Model;
using System.Collections.Generic;

namespace ParkingApp.Repository
{
    public interface IParkingRepository
    {
        List<ParkingSpot> GetEmptySpots();
        ParkingSpot GetSpotWithOneMotorcycle();
        List<ParkingSpot> GetSpotsForVan();
        void RemoveVehicle(int vehicleId);
        List<ParkingSpot> AddVehicle(Vehicle vehicle);
    }
}
