using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Handlers;
using ToDoList.Core.Models;

namespace ToDoList.Core.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TodoItems
        [HttpGet]
        public Task<IActionResult> GetTodoItems()
        {
            return _mediator.Send(new GetTodoListHandler.GetTodoList());
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetTodoItem(long id)
        {
            return _mediator.Send(new GetTodoByIdHandler.GetTodoById(id));
        }

        [HttpPut("{id}")]
        public Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            return _mediator.Send(new PutTodoHandler.PutTodo(id, todoItemDTO));
        }

        [HttpPost]
        public Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return _mediator.Send(new CreateTodoHandler.CreateTodo(todoItemDTO));
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteTodoItem(long id)
        {
            return _mediator.Send(new DeleteTodoHandler.DeleteTodo(id));
        }
    }
}