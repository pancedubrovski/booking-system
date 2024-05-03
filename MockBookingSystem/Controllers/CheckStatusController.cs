using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Services.Interfaces;

namespace MockBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckStatusController : ControllerBase
    {
        private readonly ICheckStatusManager _checkStatusService;

        public CheckStatusController(ICheckStatusManager checkStatusService)
        {
            _checkStatusService = checkStatusService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckStatusBooking([FromBody] CheckStatusReq query)
        {
            var manager = _checkStatusService.CheckHotelStatus(query);

            var reservation = await manager.CheckStatus(query);

            return Ok(reservation);

        }
    }
}
