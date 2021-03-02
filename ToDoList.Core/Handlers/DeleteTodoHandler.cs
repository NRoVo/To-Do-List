using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;

namespace ToDoList.Core.Handlers
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoHandler.DeleteTodo, IActionResult>
    {
        private readonly TodoContext _context;

        public DeleteTodoHandler(TodoContext context)
        {
            _context = context;
        }

        public class DeleteTodo : IRequest<IActionResult>
        {
            public DeleteTodo(long id)
            {
                Id = id;
            }

            public long Id { get; }
        }

        public async Task<IActionResult> Handle(DeleteTodo request, CancellationToken cancellationToken) 
        {
            var todoItem = await _context.TodoItems.FindAsync(request.Id);

            if (todoItem == null)
            {
                return new NotFoundResult();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync(cancellationToken);

            return new NoContentResult();
        }
    }
}