using HotelBookingAPI.Context;
using HotelBookingAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(HotelContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Read), new {id = booking.Id}, booking);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ReadAll()
        {
            var bookings = await context.Bookings.ToListAsync();
            return Ok(bookings);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Read(int id)
        {
            var booking = await context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
            if(booking is null) {return NotFound();}
            return Ok(booking);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Booking updatedBooking)
        {
            var bookingToUpdate = await context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
            if (bookingToUpdate is null) { return NotFound(); }
            bookingToUpdate.GuestId = updatedBooking.GuestId;
            bookingToUpdate.RoomId  = updatedBooking.RoomId;
            bookingToUpdate.CheckInDate = updatedBooking.CheckInDate;
            bookingToUpdate.CheckOutDate = updatedBooking.CheckOutDate;
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
