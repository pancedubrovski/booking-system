using MockBookingSystem.Models;

namespace MockBookingSystem.Repositories.Itnerfaces
{
    public interface IReservationRepository
    {
        public Reservation ReserveHotel(DateTime fromDate, DateTime toDate, string optionCode);

        public void ChangeReservationStauts(string bookingCode, BookingStatusEnum status);
        public Reservation GetReservationByCode(string bookingCode);
    }
}
