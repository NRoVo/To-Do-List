using System.Collections.Generic;
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

        private class PagedResult
        {
            private List<TodoItemDTO> _dtoList;
            private int _pageCount;
            public PagedResult(List<TodoItemDTO> dtoList, int pageCount)
            {
                _dtoList = dtoList;
                _pageCount = pageCount;
            }
        }

        public async Task<IActionResult> Handle(GetTodoList request, CancellationToken cancellationToken)
        {
            var result = await _context.TodoItems
               .Skip(request.PageSize * request.PageIndex)
               .Take(request.PageSize)
               .Select(x => x.ToDTO())
               .ToListAsync(cancellationToken);

            var count =  _context.TodoItems
               .Select(x => x.ToDTO())
               .Count();

            var pagedResult = new PagedResult(result, count);

            return new ObjectResult(pagedResult);
        }
    }
}