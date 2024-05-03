namespace MockBookingSystem.Utilities
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
