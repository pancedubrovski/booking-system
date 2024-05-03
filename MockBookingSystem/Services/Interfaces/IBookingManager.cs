using MockBookingSystem.Models.Queries;

namespace MockBookingSystem.Services.Interfaces
{
    public interface IBookingManager
    {
        public IManager GetBookManager(BookReq request);
    }
}
