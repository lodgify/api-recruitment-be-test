namespace ApiApplication
{
    public static class Constants
    {

        public static class Exception
        {

            public static readonly string MoviewNotFoundInIMDb = "The movie was not found in the IMDB api";
            public static readonly string PatchMethod = "This method should return an exception";
            public static readonly string CanNotUpdateShowtimeNotFound  = "The Showtime cannot be updated because it does not exist in the database";
            public static readonly string CanNotInsertShowtimeDuplicatId= "The Showtime cannot be insert because primary key already exist in database";
            public static readonly string CanNotDeleteShowtimeNotFound = "The Showtime cannot be deleted because it does not exist in the database";
            public static readonly string CanNotReadStatusTaskExecutionFrequencyInConfigurationFile = "The configuration value is incorrect for the health task. Check StatusTaskExecutionFrequency";
        }

        public static class Log
        {
       
            public static readonly string TraceFilterControllerStart = "Start execution of : ";
            public static readonly string TraceFilterControllerEnd = "Finish execution of : ";
            public static readonly string TraceScopedProcessingWorking = "Scoped Processing Service is working. Count: ";
            public static readonly string TraceScopedHostServiceRunning = "Consume Scoped Service Hosted Service running.";
            public static readonly string TraceScopedHostServiceWorking = "Consume Scoped Service Hosted Service working.";
            public static readonly string TraceScopedHostServiceStopping = "Consume Scoped Service Hosted Service stopping.";

        }
    }
}
