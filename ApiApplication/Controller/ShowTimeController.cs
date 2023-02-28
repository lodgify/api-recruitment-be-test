using ApiApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ApiApplication.Models.ViewModels;
using ApiApplication.Database;

[ApiController]
[Route("[controller]")]
public class ShowtimeController : ControllerBase
{

    private readonly IShowtimeService _service;

    public ShowtimeController(IShowtimeService service)
    {
        _service = service;
    }

    [Authorize("Read")]
    [HttpGet]
    public IActionResult Get(string title = null, DateTime? date = null)
    {
        var res = _service.GetShowTimeSchedule(title, date);

        return Ok(res);
    }

    [Authorize("Write")]
    [HttpPost]
    public async Task<IActionResult> Post(ShowtimeViewModel command)
    {
        var res = await _service.Add(command);

        return Ok(res);
    }

    [Authorize("Write")]
    [HttpPut]
    public async Task<IActionResult> Put(ShowtimeViewModel command)
    {
        var res = await _service.Update(command);

        return Ok(res);

    }

    [Authorize("Write")]
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var res = _service.DeleteTimeSchedule(id);

        return Ok(res);

    }

    [HttpPatch]
    public Task<IActionResult> Patch()
    {
        throw new InvalidOperationException(Constants.Exception.PatchMethod);
    }

}