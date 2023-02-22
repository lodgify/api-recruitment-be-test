using ApiApplication.DTOs.API;

namespace ApiApplication.Background
{
    /// <summary>
    /// Class that represents the current status information for the IMDB Api.
    /// </summary>
    public class IMDBStatusSingleton
    {
        private readonly static IMDBStatusSingleton _instance = new IMDBStatusSingleton();

        private IMDBStatusSingleton() { }

        public static IMDBStatusSingleton Instance => _instance;

        /// <summary>
        /// Current Status of IMDB Api
        /// </summary>
        public IMDBStatus Status { get; set; }
    }
}
