namespace MockBookingSystem.Models
{
    public  static class DataSource
    {
        public static List<Reservation> reservations { get; set; } = new List<Reservation>();

        public static List<Arrangement> arrangements { get; set; } = new List<Arrangement>();

        public static List<Hotel> hotels { get; set; } = new List<Hotel>();

        public static List<Flight> flights { get; set; } = new List<Flight>();
    }
}
