using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;
using ToDoList.Core.Extensions;

namespace ToDoList.Core.Handlers
{
   
    public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdHandler.GetTodoById, IActionResult>
    {
        private readonly TodoContext _context;

        public GetTodoByIdHandler(TodoContext context)
        {
            _context = context;
        }

        public class GetTodoById : IRequest<IActionResult>
        {
            public GetTodoById(long id)
            {
                Id = id;
            }

            public long Id { get; }
        }

        public async Task<IActionResult> Handle(GetTodoById request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems.FindAsync(request.Id);

            if (todoItem == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(todoItem.ToDTO());
        }
    }
}