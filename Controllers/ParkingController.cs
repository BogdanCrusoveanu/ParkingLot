using Microsoft.AspNetCore.Mvc;
using ParkingApp.Model;
using ParkingApp.Repository;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingController(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        // GET: api/<ParkingController>
        [HttpGet("empty")]
        public IActionResult GetEmptySpot()
        {
            return Ok(_parkingRepository.GetEmptySpots());
        }

        [HttpGet("empty/van")]
        public IActionResult GetEmptySpotsForVan()
        {
            return Ok(_parkingRepository.GetSpotsForVan());
        }

        [HttpGet("empty/motorcycle")]
        public IActionResult GetSpotsWithMotorcycles()
        {
            return Ok(_parkingRepository.GetSpotWithOneMotorcycle());
        }

        // POST api/<ParkingController>
        [HttpPost("add")]
        public IActionResult Post(Vehicle vehicle)
        {
            var spots = _parkingRepository.AddVehicle(vehicle);

            if(spots == null )
            {
                return BadRequest("All the parking spots are full");
            }
            string result = "Your spots are: ";
            foreach (var spot in spots)
            {
                result = result + " " + spot.ParkingSpotId;
            }
            return Ok(result);
        }

        // PUT api/<ParkingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ParkingController>/5
        [HttpDelete("{vehicleId}")]
        public IActionResult Delete(int vehicleId)
        {
            _parkingRepository.RemoveVehicle(vehicleId);

            return Ok("Vehicle " + vehicleId + " left the parking lot!");
        }
    }
}
