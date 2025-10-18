using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdSetsController : ControllerBase
{
    private readonly IAdSetService _service;

    public AdSetsController(IAdSetService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdSetDto>>> GetAll()
    {
        var adSets = await _service.GetAllAsync();
        return Ok(adSets);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<AdSetDto>>> GetByAgency(Guid agencyId)
    {
        var adSets = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(adSets);
    }

    [HttpGet("by-campaign/{campaignId}")]
    public async Task<ActionResult<IReadOnlyList<AdSetDto>>> GetByCampaign(Guid campaignId)
    {
        var adSets = await _service.GetByCampaignIdAsync(campaignId);
        return Ok(adSets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdSetDto>> GetById(Guid id)
    {
        var adSet = await _service.GetByIdAsync(id);
        if (adSet == null)
            return NotFound();

        return Ok(adSet);
    }

    [HttpPost]
    public async Task<ActionResult<AdSetDto>> Create([FromBody] CreateAdSetDto dto)
    {
        var adSet = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = adSet.Id }, adSet);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AdSetDto>> Update(Guid id, [FromBody] UpdateAdSetDto dto)
    {
        var adSet = await _service.UpdateAsync(id, dto);
        if (adSet == null)
            return NotFound();

        return Ok(adSet);
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
