using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;
using ToDoList.Core.Models;

namespace ToDoList.Core.Handlers
{
    public class PutTodoHandler : IRequestHandler<PutTodoHandler.PutTodo, IActionResult>
    {
        private readonly TodoContext _context;

        public PutTodoHandler(TodoContext context)
        {
            _context = context;
        }

        public class PutTodo : IRequest<IActionResult>
        {
            public PutTodo(long id, TodoItemDTO dto)
            {
                Id = id;
                DTO = dto;
            }

            public long Id { get; }
            public TodoItemDTO DTO { get; }
        }

        public async Task<IActionResult> Handle(PutTodo request, CancellationToken cancellationToken)
        {
            if (request.Id != request.DTO.Id)
            {
                return new BadRequestResult();
            }

            var todoItem = await _context.TodoItems.FindAsync(request.Id);
            if (todoItem == null)
            {
                return new NotFoundResult();
            }

            todoItem.Name = request.DTO.Name;
            todoItem.IsComplete = request.DTO.IsComplete;

            await _context.SaveChangesAsync(cancellationToken);

            return new NoContentResult();
        }
    }
}