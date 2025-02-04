using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        private bool TodoItemExists(int id){
            return _context.TodoItems.Any(e=>e.Id==id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id){
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem==null){
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(int id, TodoItem todoItem){
            if(id!=todoItem.Id){
                return BadRequest();
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            try{
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!TodoItemExists(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id){
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem==null){
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
    }
}