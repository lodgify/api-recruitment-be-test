using ApiApplication.Models;

namespace ApiApplication.Helpers
{
    public static class ValidatorsHelper
    {
        public static bool ShowtimeResource(Showtime showtime, out string errorMessage)
        {
            errorMessage = null;

            if (showtime is null)
            {
                errorMessage = "Invalid showtime data";
                return false;
            }
            if (showtime.AuditoriumId <= 0){
                errorMessage = "AuditoriumId cannot be less than Zero or Zero";
                return false;
            }

            if (showtime.StartDate >= showtime.EndDate)
            {
                errorMessage = "Invalid date range. StartDate cannot be greater than or equal to EndDate.";
                return false;
            }
            return true;
        }
    }
}
