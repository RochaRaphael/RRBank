using Microsoft.AspNetCore.Mvc;
using RRBank.Application.Model.ModelIn;
using RRBank.Application.Services;

namespace RRBank.Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _service;
        public ManagerController(ManagerService managerService)
        {
            _service = managerService;
        }

        [HttpGet("ManagerById/{clientId:int}")]
        public async Task<IActionResult> ManagerList(int clientId)
        {
            var result = await _service.GetManagerByIdAsync(clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("ManagerList/")]
        public async Task<IActionResult> ManagerList()
        {
            var result = await _service.ManagerListAsync();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("AddManager/")]
        public async Task<IActionResult> AddManager(AddManagerIn newManager)
        {
            var result = await _service.AddManagerAsync(newManager);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update/{managerId:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int managerId,
            [FromBody] UpdateManagerIn newManager)
        {
            var result = await _service.UpdateManagerAsync(newManager, managerId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete/{managerId:int}")]
        public async Task<IActionResult> DeleteManager(
            [FromRoute] int managerId)
        {
            var result = await _service.DeleteManagerAsync(managerId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("CreateAccount/{clientId:int}")]
        public async Task<IActionResult> CreateAccount(
            [FromRoute] int clientId)
        {
            var result = await _service.CreateAccountAsync(clientId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
