using Lodgify.Cinema.Domain.Contract;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public class ProjectEnvinronmentConfiguration : IProjectEnvinronmentConfiguration
    {
        public bool Application_EnableOptionalPagination { get; internal set; }
        public string ExternalApi_Imdb_X_RapidAPI_Key { get; internal set; }

        public string ExternalApi_Imdb_X_RapidAPI_Host { get; internal set; }

        public string ExternalApi_Imdb_BaseUri { get; internal set; }
        public string Auth_ReadOnlyToken { get; internal set; }
        public string Auth_WriteToken { get; internal set; }
    }
}