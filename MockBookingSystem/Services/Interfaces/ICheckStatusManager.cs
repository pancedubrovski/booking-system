using MockBookingSystem.Models.Queries;

namespace MockBookingSystem.Services.Interfaces
{
    public interface ICheckStatusManager
    {
        public IManager CheckHotelStatus(CheckStatusReq req);
    }
}
