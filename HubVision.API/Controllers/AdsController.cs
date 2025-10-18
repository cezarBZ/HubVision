using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdsController : ControllerBase
{
    private readonly IAdService _service;

    public AdsController(IAdService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdDto>>> GetAll()
    {
        var ads = await _service.GetAllAsync();
        return Ok(ads);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<AdDto>>> GetByAgency(Guid agencyId)
    {
        var ads = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(ads);
    }

    [HttpGet("by-adset/{adSetId}")]
    public async Task<ActionResult<IReadOnlyList<AdDto>>> GetByAdSet(Guid adSetId)
    {
        var ads = await _service.GetByAdSetIdAsync(adSetId);
        return Ok(ads);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdDto>> GetById(Guid id)
    {
        var ad = await _service.GetByIdAsync(id);
        if (ad == null)
            return NotFound();

        return Ok(ad);
    }

    [HttpPost]
    public async Task<ActionResult<AdDto>> Create([FromBody] CreateAdDto dto)
    {
        var ad = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = ad.Id }, ad);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AdDto>> Update(Guid id, [FromBody] UpdateAdDto dto)
    {
        var ad = await _service.UpdateAsync(id, dto);
        if (ad == null)
            return NotFound();

        return Ok(ad);
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
