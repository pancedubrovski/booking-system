namespace MockBookingSystem.Models
{
    public class Reservation
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string BookingCode { get; set; }
        public string OptionCode { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingStatusEnum BookingStatus { get; set; }

        public Reservation(DateTime checkInDate, DateTime checkOutDate, string bookingCode,
            string optionCode, DateTime bookingTime)
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            BookingCode = bookingCode;
            OptionCode = optionCode;
            BookingTime = bookingTime;
            BookingStatus = BookingStatusEnum.Pending;

        }
    }
}
