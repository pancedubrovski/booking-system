using MockBookingSystem.Models;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;
using System.Net.Http;

namespace MockBookingSystem.Services
{
    public class SearchManager: ISearchManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRespository _hotelRespository;
        private readonly IReservationRepository _reservationRepository;

        public SearchManager(IHttpClientWrapper httpClientWrapper, IFlightRepository flightRepository, IHotelRespository hotelRespository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _flightRepository = flightRepository;
            _hotelRespository = hotelRespository;
            _reservationRepository = reservationRepository;
        }

        public IManager GetSearchManager(SearchReq request)
        {
            if (!string.IsNullOrEmpty(request.DepartureAirport))
            {
                return new HotelAndFlightSearchManager(_httpClientWrapper,_flightRepository,_hotelRespository, _reservationRepository);
            }
            else if ((request.FromDate - DateTime.Today).TotalDays <= 45)
            {
                return new LastMinuteHotelsSearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);
            }
            else
            {
                return new HotelOnlySearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);
            }
        }

      
    }
}
