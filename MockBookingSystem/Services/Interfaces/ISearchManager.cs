using MockBookingSystem.Models.Queries;

namespace MockBookingSystem.Services.Interfaces
{
    public interface ISearchManager
    {
        public IManager GetSearchManager(SearchReq request);
    }
}
