using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;
using ToDoList.Core.Extensions;
using ToDoList.Core.Models;

namespace ToDoList.Core.Handlers
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoHandler.CreateTodo, IActionResult>
    {
        private readonly TodoContext _context;

        public CreateTodoHandler(TodoContext context)
        {
            _context = context;
        }

        public class CreateTodo : IRequest<IActionResult>
        {
            public CreateTodo(TodoItemDTO dto)
            {
                DTO = dto;
            }

            public TodoItemDTO DTO { get; }
        }

        public async Task<IActionResult> Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                IsComplete = request.DTO.IsComplete,
                Name = request.DTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync(cancellationToken);

            return new ObjectResult(todoItem.ToDTO()) {StatusCode = StatusCodes.Status201Created};
        }
    }
}