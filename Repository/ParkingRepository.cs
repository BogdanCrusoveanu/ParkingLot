using ParkingApp.Data;
using ParkingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingApp.Repository
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly DataContext _context;

        public ParkingRepository(DataContext context)
        {
            _context = context;
        }

        public List<ParkingSpot> AddVehicle(Vehicle vehicle)
        {
            var vehicleType = _context.VehicleTypes.Where(t => t.VehicleTypeId == vehicle.VehicleTypeId).FirstOrDefault();
            var spots = new List<ParkingSpot>();
            switch (vehicleType.Type)
            {
                case "Motorcycle":
                    var emptySpots = this.GetEmptySpots();
                    var motorcycleSpot = this.GetSpotWithOneMotorcycle();
                    if (motorcycleSpot != null)
                    {
                        _context.Vehicles.Add(vehicle);
                        _context.SaveChanges();
                        _context.Add(new VehicleSpot(vehicle.VehicleId, motorcycleSpot.ParkingSpotId));
                        _context.SaveChanges();
                        spots.Add(motorcycleSpot);
                        return spots;
                    }
                    else
                    {
                        if (emptySpots != null)
                        {
                            _context.Vehicles.Add(vehicle);
                            _context.SaveChanges();
                            _context.Add(new VehicleSpot(vehicle.VehicleId, emptySpots[0].ParkingSpotId));
                            _context.SaveChanges();
                            spots.Add(emptySpots[0]);
                            return spots;
                        }
                        else
                        {
                            return null;
                        }
                    }
                case "Car":
                    emptySpots = this.GetEmptySpots();
                    if (emptySpots != null)
                    {
                        _context.Vehicles.Add(vehicle);
                        _context.SaveChanges();
                        _context.Add(new VehicleSpot(vehicle.VehicleId, emptySpots[0].ParkingSpotId));
                        _context.SaveChanges();
                        spots.Add(emptySpots[0]);
                        return spots;
                    }
                    else
                    {
                        return null;
                    }
                case "Van":
                    emptySpots = this.GetEmptySpots();
                    if (emptySpots != null)
                    {
                        if (emptySpots.Count >= 2)
                        {
                            emptySpots = emptySpots.Take(2).ToList();
                            _context.Vehicles.Add(vehicle);
                            _context.SaveChanges();
                            foreach (var vanSpot in emptySpots)
                            {
                                _context.Add(new VehicleSpot(vehicle.VehicleId, vanSpot.ParkingSpotId));
                                _context.SaveChanges();
                            }
                            return emptySpots;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return null;
                default:
                    return null;
            }
        }

        public List<ParkingSpot> GetEmptySpots()
        {
            var spots = _context.ParkingSpots
             .Where(c => !_context.VehicleSpots
             .Select(b => b.ParkingSpotId)
             .Contains(c.ParkingSpotId)).ToList();
            if (spots.Count > 0)
            {
                return spots;
            }
            else
            {
                return null;
            }
        }

        public List<ParkingSpot> GetSpotsForVan()
        {
            var spots = _context.ParkingSpots
            .Where(c => !_context.VehicleSpots
            .Select(b => b.ParkingSpotId)
            .Contains(c.ParkingSpotId)).Take(2).ToList();
            return spots;
        }

        public ParkingSpot GetSpotWithOneMotorcycle()
        {
            var spots = (from vs in _context.VehicleSpots
                         join s in _context.ParkingSpots on vs.ParkingSpotId equals s.ParkingSpotId
                         join v in _context.Vehicles on vs.VehicleId equals v.VehicleId
                         join vt in _context.VehicleTypes on v.VehicleTypeId equals vt.VehicleTypeId
                         where vt.Type == "Motorcycle"
                         select s).ToList();

            var availableSpot =
                (from g in spots.GroupBy(x => x)
                 where g.Count() == 1
                 select g.First()).FirstOrDefault();

            return availableSpot;
        }

        public void RemoveVehicle(int vehicleId)
        {
            var vehicle = _context.Vehicles.Find(vehicleId);

            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }

        }
    }
}
