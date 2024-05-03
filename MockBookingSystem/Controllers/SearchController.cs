using Microsoft.AspNetCore.Mvc;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Services.Interfaces;


namespace MockBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchManager _searchService;

        public SearchController(ISearchManager searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> serachHotels([FromQuery] SearchReq query)
        {
            var manager  = _searchService.GetSearchManager(query);

            var list = await manager.Search(query);

            return Ok(list);    
        }
    }
}
