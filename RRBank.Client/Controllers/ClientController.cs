using Microsoft.AspNetCore.Mvc;
using RRBank.Application.Services;
using RRBank.Domain.Database;
using RRBank.Application.ViewModel;

namespace RRBank.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;
        public ClientController(ClientService clientService) 
        {
            _service = clientService;
        }

        [HttpGet("ClientById/{clientId:int}")]
        public async Task<IActionResult> ClientList(int clientId)
        {
            var result = await _service.GetClientByIdAsync(clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("ClientList/")]
        public async Task<IActionResult> ClientList()
        {
            var result = await _service.ClientListAsync();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("AddClient/")]
        public async Task<IActionResult> AddClient(NewClientViewModel newClient)
        {
            var result = await _service.AddClientAsync(newClient);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update/{clientId:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int clientId,
            [FromBody] UpdateClientViewModel newClient)
        {
            var result = await _service.UpdateClientAsync(newClient, clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete/{clientId:int}")]
        public async Task<IActionResult> DeleteClientAsync(
            [FromRoute] int clientId)
        {
            var result = await _service.DeleteClientAsync(clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
