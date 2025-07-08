using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.TodoItemDTOs;

namespace todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItem()
        {
            var list = await _service.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
           var item = await _service.GetTodoItem(id);

           if (item == null) 
               return NotFound();
           
           else
           {
               return Ok(item);
           }

        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItemRequest request)
        {
            var item = await _service.CreateTodoItem(request);

            return Ok(item);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] TodoItemRequest request)
        {
            var item = await _service.UpdateTodoItem(id,request);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _service.DeleteTodoItem(id);
            return NoContent();
        }
    }
}
