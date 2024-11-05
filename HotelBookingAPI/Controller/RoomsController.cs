using HotelBookingAPI.Context;
using HotelBookingAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController(HotelContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            context.Rooms.Add(room);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Read), new { id = room.Id }, room);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ReadAll()
        {
            var rooms = await context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Read(int id)
        {
            var room = await context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if (room is null) { return NotFound(); }
            return Ok(room);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Room updatedRoom)
        {
            var roomToUpdate = await context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if (roomToUpdate is null) { return NotFound(); }
            roomToUpdate.RoomNumber = updatedRoom.RoomNumber;
            roomToUpdate.Type = updatedRoom.Type;
            roomToUpdate.PricePerNight = updatedRoom.PricePerNight;
            roomToUpdate.IsAvailable = updatedRoom.IsAvailable;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookingToDelete = await context.Bookings.FirstOrDefaultAsync(bookings => bookings.Id == id);
            if (bookingToDelete is null) { return NotFound(); }

            context.Bookings.Remove(bookingToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
