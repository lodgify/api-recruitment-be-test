using ApiApplication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Lodgify.Cinema.AcceptanceTest.TestServers
{
    public static class MoviesApiTestServer
    {
        #region [prop]

        private const string ENVIRONMENT_DEVELOP = "Development";
        public static string ReadOnlyToken { get; private set; }
        public static string WriteToken { get; private set; }

        #endregion [prop]

        #region [ctor]

        static MoviesApiTestServer()
        {
            SetupEnvironment();
        }

        #endregion [ctor]

        private static void SetupEnvironment()
        {
            ReadOnlyToken = "MTIzNHxSZWFk";
            WriteToken = "Nzg5NHxXcml0ZQ==";

            Environment.SetEnvironmentVariable("Application_EnableOptionalPagination", "true");
            Environment.SetEnvironmentVariable("ExternalApi_Imdb_X-RapidAPI-Key", "ef737e6180msh0a2f786fc0fb2d6p100a0cjsn91350960a583");
            Environment.SetEnvironmentVariable("ExternalApi_Imdb_X-RapidAPI-Host", "movie-details1.p.rapidapi.com");
            Environment.SetEnvironmentVariable("ExternalApi_Imdb_BaseUri", "https://movie-details1.p.rapidapi.com/imdb_api/");
            Environment.SetEnvironmentVariable("Auth_ReadOnlyToken", ReadOnlyToken);
            Environment.SetEnvironmentVariable("Auth_WriteToken", WriteToken);

  
        }

        private static TestServer _server;
        
        /// <summary>
        /// Singleton Server
        /// </summary>
        public static TestServer Server
        {
            get
            {
                if (_server == null)
                    _server = CreateTestServer();

                return _server;
            }
        }


        private static HttpClient _client;

        /// <summary>
        /// Singleton Client
        /// </summary>
        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                    _client = CreateClient();

                return _client;
            }
        }

        private static TestServer CreateTestServer()
        {
            var server = new TestServer(new WebHostBuilder()
       .UseEnvironment(ENVIRONMENT_DEVELOP)
       .UseStartup<Startup>());

            return server;
        }

        private static HttpClient CreateClient()
        {
            var server = CreateTestServer();
            return server.CreateClient();
        }
    }
}