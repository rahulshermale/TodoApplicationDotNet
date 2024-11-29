using TodoApplication.Model;

namespace TodoApplication.Service
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<TodoItem> GetTodoByIdAsync(int id);
        Task<IEnumerable<TodoItem>> SearchTodosAsync(string keyword);
        Task AddTodoAsync(TodoItem todoItemDto);
        Task UpdateTodoAsync(int id, TodoItem todoItemDto);
        Task DeleteTodoAsync(int id);
    }
}
