using System.Text.Json.Serialization;

namespace MockBookingSystem.Models
{
    public class Flight
    {
        [JsonPropertyName("flightCode")]
        public int FlightCode { get; set; }
        
        [JsonPropertyName("flightNumber")]
        public string FlightNumber { get; set; }
        
        [JsonPropertyName("departureAirport")]
        public string DepartureAirport { get; set; }

        [JsonPropertyName("arrivalAirport")]
        public string ArrivalAirport { get; set; }

    }
}
