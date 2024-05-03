using MockBookingSystem.Models;
using MockBookingSystem.Repositories.Itnerfaces;

namespace MockBookingSystem.Repositories
{
    public class HotelRepositiroy: IHotelRespository
    {

      
        public void StoreHotels(List<Hotel> hotels)
        {
            List<int> existingHotels = DataSource.hotels
                .Select(h => h.HotelCode)
                .ToList();

            DataSource.hotels.AddRange(hotels.
                Where(h => !existingHotels.Contains(h.HotelCode)).ToList());
        }

        public List<Hotel> getAvilableHotelsByCity(string cityCode, DateTime dateFrom, DateTime dateTo)
        {

            return DataSource.hotels.Where(h => h.DestinationCode == cityCode)
                .Where(h => h.Reservations.Find(r => !IsAvilableHotel(r, dateFrom, dateTo)) == null)
                .ToList();
        }
        private bool IsAvilableHotel(Reservation reservation, DateTime CheckInDate, DateTime ChekOutDate)
        {
            if (!(reservation.CheckOutDate < CheckInDate || reservation.CheckInDate > ChekOutDate))
            {
                return false;
            }
            return true;
        }
    }
}
