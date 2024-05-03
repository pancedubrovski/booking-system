using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace MockBookingSystem.Models.Queries
{
    public class SearchReq
    {
        [BindRequired]
        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }
        public string? DepartureAirport { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "FromDate is required")]
        public DateTime FromDate { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "ToDate is required")]
        public DateTime ToDate { get; set; }

    }
}
