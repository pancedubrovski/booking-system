using MockBookingSystem.Models;

namespace MockBookingSystem.Repositories.Itnerfaces
{
    public interface IHotelRespository
    {
       
        public void StoreHotels(List<Hotel> hotels);

        public List<Hotel> getAvilableHotelsByCity(string cityCode, DateTime dateFrom, DateTime dateTo);

    }
}
