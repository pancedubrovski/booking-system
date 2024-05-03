using MockBookingSystem.Models;
using MockBookingSystem.Repositories.Itnerfaces;

namespace MockBookingSystem.Repositories
{
    public class ReservationRepositroy: IReservationRepository
    {
        public Reservation ReserveHotel(DateTime fromDate, DateTime toDate, string optionCode)
        {

            string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string bookCode = "";
            for (int i = 0; i < 10; i++)
            {
                int a = random.Next(0, Alphabet.Length);
                bookCode += Alphabet.ElementAt(a);
            }


            Reservation reservation = new Reservation(fromDate,
                toDate, bookCode, optionCode, DateTime.Now);


            Arrangement arrangement = DataSource.arrangements.Find(a => a.OptionCode == optionCode)!;
            Hotel hotel= DataSource.hotels.Find(h => h.HotelCode == arrangement.HotelCode)!;
            hotel.Reservations.Add(reservation);

            DataSource.reservations.Add(reservation);

            return reservation;
        }
        public void ChangeReservationStauts(string bookingCode, BookingStatusEnum status)
        {
            DataSource.reservations.Find(r => r.BookingCode == bookingCode)!.BookingStatus = status;
        }

        public Reservation GetReservationByCode(string optionCode)
        {
           return DataSource.reservations.Find(r => r.OptionCode == optionCode)!;
        }
    }
}
