using MyApi.Models;
using System.Collections.Generic;

namespace MyApi.Services
{
    public interface ITodoService
    {
        bool TodoItemExists(int id);
        IEnumerable<TodoItem> GetAllTodoItems();
        TodoItem GetItem(int id);
        Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);
        Task<TodoItem> UpdateTodoItemAsync(int id, TodoItem todoItem);
        Task DeleteTodoItemAsync(int id);
    }
}