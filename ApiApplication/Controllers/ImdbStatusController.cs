using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTOs.Showtime;
using ApiApplication.Exceptions;
using ApiApplication.Filters;
using ApiApplication.Helpers;
using ApiApplication.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImdbStatusController : ControllerBase
    {
        private readonly ImdbStatusDto _imdbStatus;

        public ImdbStatusController(ImdbStatusDto imdbStatus)
        {
            this._imdbStatus = imdbStatus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_imdbStatus);
        }
    }
}
