using MockBookingSystem.Models;

namespace MockBookingSystem.Repositories.Itnerfaces
{
    public interface IFlightRepository
    {

        public void StoreFlights(List<Flight> flights);
        public List<Flight> GetFlightsByDestinations(string DepartureAirport, string ArrivalAirport);
    }
}
