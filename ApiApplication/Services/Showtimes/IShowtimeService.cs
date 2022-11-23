using ApiApplication.Models;
using ApiApplication.Models.Showtimes;
using System;
using System.Collections.Generic;

namespace ApiApplication.Services.Showtimes
{
    public interface IShowtimeService
    {
        ResponseModel<IList<ShowtimeModel>> GetAll(string title, DateTime date);
        ResponseModel<ShowtimeModel> GetById(int id);
        ResponseModel<ShowtimeModel> Add(AddShowtimeModel model);
        ResponseModel<ShowtimeModel> Update(ShowtimeModel model);
        ResponseModel<bool> Delete(int id);
    }
}
