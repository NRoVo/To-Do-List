using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Db;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Extensions;
using ToDoList.Core.Models;

namespace ToDoList.Core.Handlers
{
    public class GetTodoListHandler : IRequestHandler<GetTodoListHandler.GetTodoList, IActionResult>
    {
        private readonly TodoContext _context;

        public GetTodoListHandler(TodoContext context)
        {
            _context = context;
        }

        public class GetTodoList : IRequest<IActionResult>
        {
            internal readonly int PageSize;
            internal readonly int PageIndex;

            public GetTodoList(int pageSize, int pageIndex)
            {
                PageSize = pageSize;
                PageIndex = pageIndex;
            }
        }

        public async Task<IActionResult> Handle(GetTodoList request, CancellationToken cancellationToken)
        {
            var result = await _context.TodoItems
               .Skip(request.PageSize * request.PageIndex)
               .Take(request.PageSize)
               .Select(x => x.ToDTO())
               .ToListAsync(cancellationToken);

            var count = await _context.TodoItems.CountAsync(cancellationToken);

            var pagedResult = new PagedResult<TodoItemDTO>(result, count);

            return new ObjectResult(pagedResult);
        }
    }
}