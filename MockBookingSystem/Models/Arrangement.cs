namespace MockBookingSystem.Models
{
    public class Arrangement
    {
        public string OptionCode { get; set; }
        public int HotelCode { get; set; }
        public string FlightCode { get; set; }
        public string ArrivalAirport { get; set; }
        public double Price { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public ArrangementType ArrangementType { get; set; }

    }
}
