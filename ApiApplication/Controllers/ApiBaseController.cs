using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Produces("application/json")]
    public abstract class ApiBaseController : Controller
    {
        protected IMapper mapper;
        public ApiBaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}

