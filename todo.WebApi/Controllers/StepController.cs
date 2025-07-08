using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.StepDTOs;

namespace todo.WebApi.Controllers
{
    [Route("api/TodoItem/{itemId}/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepService _service;

        public StepController(IStepService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStep(int itemId)
        {
            return  Ok(await _service.GetAllStep(itemId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStep([FromRoute] int itemId, [FromRoute] int id)
        {
            var result = await _service.GetStep(itemId,id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep([FromRoute]int itemId,[FromRoute] int id)
        {
            await _service.DeleteStep(itemId, id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStep( [FromRoute]int itemId, [FromBody]StepRequest request)
        {

            var result = await _service.CreateStep(itemId, request);
            return Ok(result);
        }
    }
}
