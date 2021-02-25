using ToDoList.Core.Db;
using ToDoList.Core.Models;

namespace ToDoList.Core.Extensions
{
    public static class TodoItemExtension
    {
        public static TodoItemDTO ToDTO(this TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}