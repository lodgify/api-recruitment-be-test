using ApiApplication.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IShowTimeService
    {
        public IEnumerable<ShowTimeResponseModel> Get(DateTime? date, string movieTitle);

        public Task<ShowTimeResponseModel> Create(ShowTimeRequestModel showTime);

        public Task<ShowTimeResponseModel> Update(ShowTimeRequestModel showTime);

        public Task<ShowTimeResponseModel> Update(int id, JsonPatchDocument<ShowTimeRequestModel> showTimePatch);

        public void Delete(int id);
    }
}
