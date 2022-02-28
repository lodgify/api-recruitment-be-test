using Cinema.Business.Abstract;
using Cinema.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{

    public class StatusController : BaseApiController
    {
        IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]        
        public IActionResult GetStatus()
        {
            // Getting the current status of Imdb api 
            var response = _statusService.GetStatus();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest();
            
        }
    }
}
