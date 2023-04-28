using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        public static readonly List<Booking> _bookings = new();
        public BookingController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking)
        {
            if (!ModelState.IsValid) return BadRequest();

            _bookings.Add(newBooking);

            _messageProducer.SendingMessage<Booking>(newBooking);

            return Ok();
        }
    }
}
