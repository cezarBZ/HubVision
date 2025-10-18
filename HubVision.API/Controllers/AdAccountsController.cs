using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdAccountsController : ControllerBase
{
    private readonly IAdAccountService _service;

    public AdAccountsController(IAdAccountService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AdAccountDto>>> GetAll()
    {
        var accounts = await _service.GetAllAsync();
        return Ok(accounts);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<AdAccountDto>>> GetByAgency(Guid agencyId)
    {
        var accounts = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdAccountDto>> GetById(Guid id)
    {
        var account = await _service.GetByIdAsync(id);
        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<AdAccountDto>> Create([FromBody] CreateAdAccountDto dto)
    {
        var account = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AdAccountDto>> Update(Guid id, [FromBody] UpdateAdAccountDto dto)
    {
        try
        {
            var account = await _service.UpdateAsync(id, dto);
            if (account == null)
                return NotFound();

            return Ok(account);
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
