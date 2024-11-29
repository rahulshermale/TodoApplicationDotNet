using TodoApplication.Model;
using TodoApplication.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace TodoApplication.Service
{
    public class TodoService : ITodoService
    {

        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }
      

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task AddTodoAsync(TodoItem todoItemDto)
        {
            var todoItem = new TodoItem
            {
                Title = todoItemDto.Title,
                Description = todoItemDto.Description,
                Category = todoItemDto.Category,
                Priority = todoItemDto.Priority,
                IsCompleted = todoItemDto.IsCompleted,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(todoItem);
        }


        public async Task<IEnumerable<TodoItem>> SearchTodosAsync(string keyword)
        {
            return await _repository.SearchAsync(keyword);
        }

        public async Task UpdateTodoAsync(int id, TodoItem todoItemDto)
        {
            var existingTodo = await _repository.GetByIdAsync(id);
            if (existingTodo == null)
                throw new KeyNotFoundException("Todo item not found");

            existingTodo.Title = todoItemDto.Title;
            existingTodo.Description = todoItemDto.Description;
            existingTodo.Category = todoItemDto.Category;
            existingTodo.Priority = todoItemDto.Priority;
            existingTodo.IsCompleted = todoItemDto.IsCompleted;

            await _repository.UpdateAsync(existingTodo);
        }
        public async Task DeleteTodoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }


    //public static class TodoItemEndpoints
    //{
    //	public static void MapTodoItemEndpoints (this IEndpointRouteBuilder routes)
    //    {
    //        var group = routes.MapGroup("/api/TodoItem").WithTags(nameof(TodoItem));

    //        group.MapGet("/", () =>
    //        {
    //            return new [] { new TodoItem() };
    //        })
    //        .WithName("GetAllTodoItems")
    //        .WithOpenApi();

    //        group.MapGet("/{id}", (int id) =>
    //        {
    //            //return new TodoItem { ID = id };
    //        })
    //        .WithName("GetTodoItemById")
    //        .WithOpenApi();

    //        group.MapPut("/{id}", (int id, TodoItem input) =>
    //        {
    //            return TypedResults.NoContent();
    //        })
    //        .WithName("UpdateTodoItem")
    //        .WithOpenApi();

    //        group.MapPost("/", (TodoItem model) =>
    //        {
    //            //return TypedResults.Created($"/api/TodoItems/{model.ID}", model);
    //        })
    //        .WithName("CreateTodoItem")
    //        .WithOpenApi();

    //        group.MapDelete("/{id}", (int id) =>
    //        {
    //            //return TypedResults.Ok(new TodoItem { ID = id });
    //        })
    //        .WithName("DeleteTodoItem")
    //        .WithOpenApi();
    //    }
    //}
}
