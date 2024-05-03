using Microsoft.Extensions.Logging.Abstractions;
using MockBookingSystem.Models;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Models.Responses;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;
using System.Linq;
using System.Text.Json;

namespace MockBookingSystem.Services
{
    public class HotelOnlySearchManager : IManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IHotelRespository _hotelRespository;
        private readonly IReservationRepository _reservationRepository;

        public HotelOnlySearchManager(IHttpClientWrapper httpClientWrapper, IHotelRespository hotelRespository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _hotelRespository = hotelRespository;
            _reservationRepository = reservationRepository;
        }
        public async Task<SearchRes> Search(SearchReq request)
        {
            string url = $"{Constant.GET_HOTELS_URL}?destinationCode={request.Destination}";
            HttpResponseMessage response = await _httpClientWrapper.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch hotels. Status code: {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();
            var hotels = JsonSerializer.Deserialize<List<Hotel>>(content)!;

            _hotelRespository.StoreHotels(hotels);

            List<Hotel> freeHotels = _hotelRespository.getAvilableHotelsByCity(request.Destination, request.FromDate, request.ToDate);

            var random = new Random();
            List<Arrangement> arrangements = freeHotels.Select(h => new Arrangement
            {
                OptionCode = h.HotelCode.ToString() + random.Next(100, 999),
                HotelCode = h.HotelCode,
                FlightCode = "",
                ArrivalAirport = h.DestinationCode,
                Price = 100,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                ArrangementType = ArrangementType.OnlyHotel
            }).ToList<Arrangement>();

            DataSource.arrangements.AddRange(arrangements);


            var searchRes = new SearchRes
            {
                Options = arrangements.Select(a => new Option
                {
                    OptionCode = a.OptionCode,
                    HotelCode = a.HotelCode,
                    FlightCode = "",
                    ArrivalAirport = a.ArrivalAirport,
                    Price = a.Price,
                }).ToList<Option>()
            };

            return searchRes;
        }


     

        public Task<BookRes> Book(BookReq request)
        {

            return Task.Run(() =>
            {
                var random = new Random();
                int sleepTime = random.Next(30, 60);

                Reservation reservation = _reservationRepository.ReserveHotel(request.SearchReq.FromDate,
                    request.SearchReq.ToDate, request.OptionCode);

                System.Threading.Thread.Sleep(1000 * sleepTime);

                _reservationRepository.ChangeReservationStauts(reservation.BookingCode,BookingStatusEnum.Success);

                return new BookRes
                {
                    BookCode = reservation.BookingCode,
                    BookingTime = reservation.BookingTime
                };
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