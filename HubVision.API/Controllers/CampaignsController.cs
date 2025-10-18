using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly ICampaignService _service;

    public CampaignsController(ICampaignService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CampaignDto>>> GetAll()
    {
        var campaigns = await _service.GetAllAsync();
        return Ok(campaigns);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<CampaignDto>>> GetByAgency(Guid agencyId)
    {
        var campaigns = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(campaigns);
    }

    [HttpGet("by-ad-account/{adAccountId}")]
    public async Task<ActionResult<IReadOnlyList<CampaignDto>>> GetByAdAccount(Guid adAccountId)
    {
        var campaigns = await _service.GetByAdAccountIdAsync(adAccountId);
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CampaignDto>> GetById(Guid id)
    {
        var campaign = await _service.GetByIdAsync(id);
        if (campaign == null)
            return NotFound();

        return Ok(campaign);
    }

    [HttpPost]
    public async Task<ActionResult<CampaignDto>> Create([FromBody] CreateCampaignDto dto)
    {
        var campaign = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = campaign.Id }, campaign);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CampaignDto>> Update(Guid id, [FromBody] UpdateCampaignDto dto)
    {
        var campaign = await _service.UpdateAsync(id, dto);
        if (campaign == null)
            return NotFound();

        return Ok(campaign);
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
