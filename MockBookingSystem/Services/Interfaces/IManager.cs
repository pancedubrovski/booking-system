using MockBookingSystem.Models.Queries;
using MockBookingSystem.Models.Responses;

namespace MockBookingSystem.Services.Interfaces
{
    public interface IManager
    {

        public Task<SearchRes> Search(SearchReq req);
        public Task<BookRes> Book(BookReq request);
        public Task<CheckStatusRes>  CheckStatus(CheckStatusReq request);

    }
}
