using Cinema.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Abstract
{
    public interface IStatusService
    {
        IResult GetStatus();
        Task<IResult> SetStatus();
    }
}
