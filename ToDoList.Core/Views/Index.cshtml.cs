using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Core.Handlers;
using ToDoList.Core.Models;

namespace ToDoList.Core.Views
{
    public class IndexView : PageModel
    {
        private readonly IMediator _mediator;

        public List<TodoItemDTO> Todos { get; private set; }

        [BindProperty]
        public FormOperation FormOperation { get; set; }
        
        [BindProperty]
        public TodoItemDTO Todo { get; set; }

        public IndexView(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PageResult> OnGetAsync()
        {
            await FillTodosAsync();
            return Page();
        }

        public async Task<PageResult> OnPostAsync()
        {
            var ct = CancellationToken.None;
            object operation = FormOperation switch
            {
                FormOperation.CreateTodo => new CreateTodoHandler.CreateTodo(Todo),
                FormOperation.UpdateTodo => new PutTodoHandler.PutTodo(Todo.Id, Todo),
                FormOperation.DeleteTodo => new DeleteTodoHandler.DeleteTodo(Todo.Id),
            };
            await _mediator.Send(operation, ct);
            await FillTodosAsync();
            return Page();
        }

        private async Task FillTodosAsync()
        {
            var request = new GetTodoListHandler.GetTodoList(100, 0);
            var result = await _mediator.Send(request, CancellationToken.None);
            if (result is ObjectResult {Value: PagedResult<TodoItemDTO> pagedResult})
            {
                Todos = pagedResult.Items;
            }
        }
    }
}