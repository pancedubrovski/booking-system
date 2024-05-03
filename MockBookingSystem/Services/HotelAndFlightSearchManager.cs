using MockBookingSystem.Models;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Models.Responses;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;
using System.Net.Http;
using System.Text.Json;

namespace MockBookingSystem.Services
{
    public class HotelAndFlightSearchManager : IManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IFlightRepository _flightRepository;
        private readonly IHotelRespository _hotelRepository;
        private readonly IReservationRepository _reservationRepository;

        public HotelAndFlightSearchManager(IHttpClientWrapper httpClientWrapper,
            IFlightRepository flightRepository, IHotelRespository hotelRepository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _flightRepository = flightRepository;
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<SearchRes> Search(SearchReq request)
        {
            return await Task.Run(async () =>
            {
                var flightsTask = Task.Run(async () =>
                {


                    string url = $"{Constant.GET_FLIGHTS_URL}?departureAirport={request.DepartureAirport}&arrivalAirport={request.Destination}";

                    HttpResponseMessage flightResponse = await _httpClientWrapper.GetAsync(url);
                    string flightContent = await flightResponse.Content.ReadAsStringAsync();
                    var flights = JsonSerializer.Deserialize<List<Flight>>(flightContent);
                    _flightRepository.StoreFlights(flights!);
                    var allFlights = _flightRepository.GetFlightsByDestinations(request.DepartureAirport!, request.Destination);
                    return allFlights;
                });

                var hotelTask = Task.Run(async () =>
                {
                    var hotelSearchManager = new HotelOnlySearchManager(_httpClientWrapper,_hotelRepository, _reservationRepository);
                    return await hotelSearchManager.Search(request);
                });
                List<Task> tasks = new List<Task>();
                tasks.Add(flightsTask);
                tasks.Add(hotelTask);
                await Task.WhenAll(tasks);

                List<Arrangement> arrangements = CombineFlightsAndHotels(flightsTask.Result, hotelTask.Result.Options, request.FromDate, request.ToDate);
                DataSource.arrangements.AddRange(arrangements);
                
                
                var searchRes = new SearchRes
                {
                    Options = arrangements
                    .Select(a => new Option
                    {
                        OptionCode = a.OptionCode,
                        HotelCode = a.HotelCode,
                        FlightCode = a.FlightCode,
                        ArrivalAirport = a.ArrivalAirport,
                        Price = a.Price
                    }).ToList<Option>()
                };

                
                return searchRes;
            });
        }


        private List<Arrangement> CombineFlightsAndHotels(List<Flight> flights, List<Option> hotels,DateTime fromDate,DateTime toDate)
        {
            List<Arrangement> result = new List<Arrangement>();
            var random = new Random();
            foreach (Flight f in flights)
            {
                var hotelsByCity = hotels.Where(o => o.ArrivalAirport == f.ArrivalAirport).ToList();



                result.AddRange(hotelsByCity.Select(h => new Arrangement
                {
                    OptionCode = f.FlightCode.ToString()+random.Next(100,999),
                    HotelCode = h.HotelCode,
                    FlightCode = f.FlightCode.ToString(),
                    ArrivalAirport = f.ArrivalAirport,
                    Price = 200,
                    FromDate = fromDate,
                    ToDate = toDate,
                    ArrangementType = ArrangementType.HotelAndFlight
                }).ToArray());
            }
            return result;
        }

       
       

        public Task<BookRes> Book(BookReq request)
        {
            return Task.Run(async () =>
            {
                var hotelSearchManager = new HotelOnlySearchManager(_httpClientWrapper, _hotelRepository, _reservationRepository);
                return await hotelSearchManager.Book(request);
            });
        }

        public async Task<CheckStatusRes> CheckStatus(CheckStatusReq request)
        {
            return await Task.Run(() =>
            {
                Reservation reservation =
                DataSource.reservations.Find(a => a.BookingCode == request.BookingCode)!;

                return new CheckStatusRes { Status = reservation.BookingStatus.ToString() };
            });
        }
    }
}
