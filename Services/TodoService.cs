using MyApi.Data;
using MyApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem GetItem(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(e => e.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"TodoItem with id {id} not found.");
            }
            return item;
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateTodoItemAsync(int id, TodoItem todoItem)
        {
            var existingItem = await _context.TodoItems.FindAsync(id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"TodoItem with id {id} not found.");
            }

            existingItem.Name = todoItem.Name;
            existingItem.IsComplete = todoItem.IsComplete;

            _context.TodoItems.Update(existingItem);
            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task DeleteTodoItemAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException($"TodoItem with id {id} not found.");
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

    }
}