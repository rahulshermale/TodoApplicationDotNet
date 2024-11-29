using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Model;
using TodoApplication.Service;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoApplicationController : ControllerBase
    {

        private readonly ITodoService _todoService;

        public TodoApplicationController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TodoItem todoItemDto)
        {
            await _todoService.AddTodoAsync(todoItemDto);
            return CreatedAtAction(nameof(GetAll), null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItem todoItemDto)
        {
            try
            {
                await _todoService.UpdateTodoAsync(id, todoItemDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var todos = await _todoService.SearchTodosAsync(keyword);
            return Ok(todos);
        }
    }
}
