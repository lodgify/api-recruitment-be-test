using Lodgify.Cinema.Domain.Contract.Log;
using System;

namespace Lodgify.Cinema.DomainService.Log
{
    public class LodgifyLogService : ILodgifyLogService
    {
        public void Log(string message)
        {
            Console.Write(message);
        }
    }
}