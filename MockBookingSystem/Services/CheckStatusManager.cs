using MockBookingSystem.Models.Queries;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;

namespace MockBookingSystem.Services
{
    public class CheckStatusManager : ICheckStatusManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRespository _hotelRespository;
        private readonly IReservationRepository _reservationRepository;

        public CheckStatusManager(IHttpClientWrapper httpClientWrapper, IFlightRepository flightRepository, IHotelRespository hotelRespository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _flightRepository = flightRepository;
            _hotelRespository = hotelRespository;
            _reservationRepository = reservationRepository;
        }

        public IManager CheckHotelStatus(CheckStatusReq request)
        {
            return new HotelOnlySearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);
        }
    }
}
