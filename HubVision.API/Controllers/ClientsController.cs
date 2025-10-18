using HubVision.Application.DTOs;
using HubVision.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HubVision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;

    public ClientsController(IClientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll()
    {
        var clients = await _service.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetByAgency(Guid agencyId)
    {
        var clients = await _service.GetByAgencyIdAsync(agencyId);
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id)
    {
        var client = await _service.GetByIdAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientDto dto)
    {
        var client = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClientDto>> Update(Guid id, [FromBody] UpdateClientDto dto)
    {
        var client = await _service.UpdateAsync(id, dto);
        if (client == null)
            return NotFound();

        return Ok(client);
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
