using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Services.Interfaces;

namespace MockBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookingManager _bookingManager;

        public BookController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<IActionResult> bookHotelAsync([FromBody] BookReq query)
        {
            var manager = _bookingManager.GetBookManager(query);
            var reservation = await manager.Book(query);
            return Ok(reservation);
        }
    }
}
