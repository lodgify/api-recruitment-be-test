using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IIMDBHttpClientManager
    {
        Task<JObject> GetIMDBJObject(string imdbID);
       
    }
}
