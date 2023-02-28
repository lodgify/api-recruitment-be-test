namespace Lodgify.Cinema.Domain.Contract
{
    public interface IProjectEnvinronmentConfiguration
    {
        bool Application_EnableOptionalPagination { get;  }
        string ExternalApi_Imdb_X_RapidAPI_Key { get;  }

        string ExternalApi_Imdb_X_RapidAPI_Host { get;  }

        string ExternalApi_Imdb_BaseUri { get;  }
        string Auth_ReadOnlyToken { get;  }
        string Auth_WriteToken { get;  }
    }
}