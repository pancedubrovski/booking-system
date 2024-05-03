using MockBookingSystem.Models.Queries;
using MockBookingSystem.Models;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;

namespace MockBookingSystem.Services
{
    public class BookingManager: IBookingManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRespository _hotelRespository;
        private readonly IReservationRepository _reservationRepository;

        public BookingManager(IHttpClientWrapper httpClientWrapper, IFlightRepository flightRepository, IHotelRespository hotelRespository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _flightRepository = flightRepository;
            _hotelRespository = hotelRespository;
            _reservationRepository = reservationRepository;
        }
        public IManager GetBookManager(BookReq request)
        {
            var arrangement = DataSource.arrangements.Find(a => a.OptionCode == request.OptionCode)!;

            switch (arrangement.ArrangementType)
            {
                case ArrangementType.HotelAndFlight:
                    return new HotelAndFlightSearchManager(_httpClientWrapper, _flightRepository, _hotelRespository, _reservationRepository);


                case ArrangementType.LastMinute:
                    return new LastMinuteHotelsSearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);

                case ArrangementType.OnlyHotel:
                    return new HotelOnlySearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);
                default: return null;
            }

        }
    }
}
