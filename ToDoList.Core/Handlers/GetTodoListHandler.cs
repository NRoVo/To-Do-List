using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Extensions;

namespace ToDoList.Core.Handlers
{
    public class GetTodoListHandler : IRequestHandler<GetTodoListHandler.GetTodoList, IActionResult>
    {
        private readonly TodoContext _context;

        public GetTodoListHandler(TodoContext context)
        {
            _context = context;
        }

        public class GetTodoList : IRequest <IActionResult>
        {
        }

        public async Task<IActionResult> Handle(GetTodoList request, CancellationToken cancellationToken)
        {
            var result = await _context.TodoItems
               .Select(x => x.ToDTO())
               .ToListAsync(cancellationToken);
            return new ObjectResult(result);
        }
    }
}