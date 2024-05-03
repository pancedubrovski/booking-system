using System.Text.Json;
using System.Text.Json.Serialization;

namespace MockBookingSystem.Models
{
    public class Hotel
    {
       

        public int Id { get; set; }

        [JsonPropertyName("hotelCode")]
        public int HotelCode { get; set; }

        [JsonPropertyName("hotelName")]
        public string HotelName { get; set; }

        [JsonPropertyName("destinationCode")]
        public string DestinationCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        public List<Reservation> Reservations { get; set; }

        public Hotel(int id, int hotelCode, string hotelName, string destinationCode, string city)
        {
            Id = id;
            HotelCode = hotelCode;
            HotelName = hotelName;
            DestinationCode = destinationCode;
            City = city;
            Reservations = new List<Reservation>();
        }
    }
}
