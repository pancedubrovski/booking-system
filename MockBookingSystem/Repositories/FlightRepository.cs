using MockBookingSystem.Models;
using MockBookingSystem.Repositories.Itnerfaces;

namespace MockBookingSystem.Repositories
{
    public class FlightRepository: IFlightRepository
    {

        public void StoreFlights(List<Flight> flights)
        {
            List<int> existingFlights = DataSource.flights
                .Select(h => h.FlightCode)
                .ToList();

            DataSource.flights.AddRange(flights.
                Where(f => !existingFlights.Contains(f.FlightCode)).ToList());
        }
        public List<Flight> GetFlightsByDestinations(string DepartureAirport, string ArrivalAirport)
        {
            return DataSource.flights.Where(f => f.ArrivalAirport == ArrivalAirport
            && f.DepartureAirport == DepartureAirport).ToList();
        }
    }
}
