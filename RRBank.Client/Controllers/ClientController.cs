using Microsoft.AspNetCore.Mvc;
using RRBank.Application.Services;
using RRBank.Domain.Database;
using RRBank.Application.Model.ModelIn;
using Azure.Core;

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

        [HttpGet("ClientListPaginated/{page:int}/{pageSize:int}/")]
        public async Task<IActionResult> ClientListPaginated([FromRoute] int page, int pageSize)
        {
            var result = await _service.ClientListPaginatedAsync(page, pageSize, null);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("ClientListPaginated/{page:int}/{pageSize:int}/{search}")]
        public async Task<IActionResult> ClientListPaginated([FromRoute] int page, int pageSize, string search)
        {
            var result = await _service.ClientListPaginatedAsync(page, pageSize, search);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("AddClient/")]
        public async Task<IActionResult> AddClient(AddClientIn newClient)
        {
            var result = await _service.AddClientAsync(newClient);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update/{clientId:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int clientId,
            [FromBody] UpdateClientIn newClient)
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

        [HttpPost("RequestCancellation/{clientId:int}")]
        public async Task<IActionResult> RequestCancellation(int clientId)
        {
            var result = await _service.RequestCancellationAsync(clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
