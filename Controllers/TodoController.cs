using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        private bool TodoItemExists(int id){
            return _todoService.TodoItemExists(id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return Ok(_todoService.GetAllTodoItems());
        }
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id){
            
            var item = _todoService.GetItem(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var createdItem = await _todoService.AddTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItems), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem){
            
            var updatedItem = await _todoService.UpdateTodoItemAsync(id, todoItem);
            return Ok(updatedItem);
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id){
           
            await _todoService.DeleteTodoItemAsync(id);
            return NoContent();
        }
    
    }
}