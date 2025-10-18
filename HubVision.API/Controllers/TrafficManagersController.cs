using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrafficManagersController : ControllerBase
{
    private readonly ITrafficManagerService _service;

    public TrafficManagersController(ITrafficManagerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TrafficManagerDto>>> GetAll()
    {
        var managers = await _service.GetAllAsync();
        return Ok(managers);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<TrafficManagerDto>>> GetByAgency(Guid agencyId)
    {
        var managers = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(managers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrafficManagerDto>> GetById(Guid id)
    {
        var manager = await _service.GetByIdAsync(id);
        if (manager == null)
            return NotFound();

        return Ok(manager);
    }

    [HttpPost]
    public async Task<ActionResult<TrafficManagerDto>> Create([FromBody] CreateTrafficManagerDto dto)
    {
        try
        {
            var manager = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = manager.Id }, manager);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrafficManagerDto>> Update(Guid id, [FromBody] UpdateTrafficManagerDto dto)
    {
        try
        {
            var manager = await _service.UpdateAsync(id, dto);
            if (manager == null)
                return NotFound();

            return Ok(manager);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
