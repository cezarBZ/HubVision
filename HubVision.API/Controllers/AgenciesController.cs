using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgenciesController : ControllerBase
{
    private readonly IAgencyService _service;

    public AgenciesController(IAgencyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AgencyDto>>> GetAll()
    {
        var agencies = await _service.GetAllAsync();
        return Ok(agencies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgencyDto>> GetById(Guid id)
    {
        var agency = await _service.GetByIdAsync(id);
        if (agency == null)
            return NotFound();

        return Ok(agency);
    }

    [HttpPost]
    public async Task<ActionResult<AgencyDto>> Create([FromBody] CreateAgencyDto dto)
    {
        try
        {
            var agency = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = agency.Id }, agency);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AgencyDto>> Update(Guid id, [FromBody] UpdateAgencyDto dto)
    {
        try
        {
            var agency = await _service.UpdateAsync(id, dto);
            if (agency == null)
                return NotFound();

            return Ok(agency);
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
