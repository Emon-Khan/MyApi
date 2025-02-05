using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApi.Dto;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodoController(IMapper mapper, ITodoService todoService)
        {
            _mapper = mapper;
            _todoService = todoService;
        }

        private bool TodoItemExists(int id)
        {
            return _todoService.TodoItemExists(id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            var items = _todoService.GetAllTodoItems();

            var itemDtos = _mapper.Map<IEnumerable<TodoItemDto>>(items); // Map to DTOs
            return Ok(itemDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {

            var item = _todoService.GetItem(id);
            var itemDto = _mapper.Map<TodoItemDto>(item); // Map to DTO
            return Ok(itemDto);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDto); // Map DTO to entity
            var createdItem = await _todoService.AddTodoItemAsync(todoItem);
            var createdItemDto = _mapper.Map<TodoItemDto>(createdItem); // Map created entity back to DTO
            return CreatedAtAction(nameof(GetTodoItems), new { id = createdItem.Id }, createdItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItemDto todoItemDto)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDto); // Map DTO to entity

            var updatedItem = await _todoService.UpdateTodoItemAsync(id, todoItem);
            return Ok(updatedItem);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {

            await _todoService.DeleteTodoItemAsync(id);
            return NoContent();
        }

    }
}