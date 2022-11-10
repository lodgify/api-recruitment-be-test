using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IShowTimeService
    {
        IEnumerable<ShowTimeDTO> GetCollection();
        IEnumerable<ShowTimeDTO> GetCollection(ShowTimeCriteriaDTO criteria);
        Task<ShowTimeDTO> Create(ShowTimeDTO showTimeDTO);
        Task<ShowTimeDTO> Update(ShowTimeDTO showTimeDTO);

    }
}
