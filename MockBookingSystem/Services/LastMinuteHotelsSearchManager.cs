using MockBookingSystem.Models;
using MockBookingSystem.Models.Queries;
using MockBookingSystem.Models.Responses;
using MockBookingSystem.Repositories.Itnerfaces;
using MockBookingSystem.Services.Interfaces;
using MockBookingSystem.Utilities;

namespace MockBookingSystem.Services
{
    public class LastMinuteHotelsSearchManager : IManager
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IHotelRespository _hotelRespository;
        private readonly IReservationRepository _reservationRepository;
        public LastMinuteHotelsSearchManager(IHttpClientWrapper httpClientWrapper, IHotelRespository hotelRespository, IReservationRepository reservationRepository)
        {
            _httpClientWrapper = httpClientWrapper;
            _hotelRespository = hotelRespository;
            _reservationRepository = reservationRepository;
        }



        public async Task<SearchRes> Search(SearchReq request)
        {
            return await Task.Run(async () =>
            {
                var hotelSearchManager = new HotelOnlySearchManager(_httpClientWrapper, _hotelRespository, _reservationRepository);
                var res=  await hotelSearchManager.Search(request);
                var random = new Random();
                List<Arrangement> arrangements = res.Options.Select(h => new Arrangement
                {
                    OptionCode = h.HotelCode.ToString() + random.Next(100, 999),
                    HotelCode = h.HotelCode,
                    FlightCode = "",
                    ArrivalAirport = request.Destination,
                    Price = 100,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    ArrangementType = ArrangementType.LastMinute
                }).ToList<Arrangement>();

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

                DataSource.arrangements.AddRange(arrangements);
                return searchRes;
            });
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

                _reservationRepository.ChangeReservationStauts(reservation.BookingCode, BookingStatusEnum.Failed);

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
