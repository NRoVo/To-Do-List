using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ToDoList.Core.Handlers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Models;

namespace ToDoList.UnitTests
{
    public class HandlersTests : DatabaseInvolvingTests
    {
        private readonly GetTodoListHandler _getTodoListHandler;
        private readonly GetTodoByIdHandler _getTodoByIdHandler;
        private readonly CreateTodoHandler _createTodoHandler;
        private readonly PutTodoHandler _putTodoHandler;
        private readonly DeleteTodoHandler _deleteTodoHandler;

        public HandlersTests()
        {
            var dbContext = DbContextCreate();
            _getTodoListHandler = new(dbContext);
            _createTodoHandler = new(dbContext);
            _deleteTodoHandler = new(dbContext);
            _putTodoHandler = new(dbContext);
            _getTodoByIdHandler = new(dbContext);
        }

        [Fact]
        public async Task CheckGetTodoListHandler()
        {
            var pageSize = 10;
            var pageIndex = 0;
            var dto = new TodoItemDTO();

            var getTodoListRequest = new GetTodoListHandler.GetTodoList(pageSize, pageIndex);
            var getTodoListResult = await _getTodoListHandler.Handle(getTodoListRequest, CancellationToken.None);

            getTodoListResult.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult) getTodoListResult;
            objectResult.Value.Should().BeOfType<PagedResult<TodoItemDTO>>().Which.Items.Should()
               .BeOfType<List<TodoItemDTO>>();

            objectResult.Value.Should().BeOfType<PagedResult<TodoItemDTO>>().Which.Count.Should().Be(0);
            objectResult.StatusCode.Should().BeNull();

            var createTodoRequest = new CreateTodoHandler.CreateTodo(dto);
            await _createTodoHandler.Handle(createTodoRequest, CancellationToken.None);

            getTodoListRequest = new GetTodoListHandler.GetTodoList(pageSize, pageIndex);
            getTodoListResult = await _getTodoListHandler.Handle(getTodoListRequest, CancellationToken.None);

            var newObjectResult = (ObjectResult) getTodoListResult;
            newObjectResult.Value.Should().BeOfType<PagedResult<TodoItemDTO>>().Which.Count.Should().Be(1);
        }

        [Fact]
        public async Task CheckGetTodoByIdHandler()
        {
            var dto = new TodoItemDTO();
            var createTodoRequest = new CreateTodoHandler.CreateTodo(dto);
            await _createTodoHandler.Handle(createTodoRequest, CancellationToken.None);

            var getTodoByIdrequest = new GetTodoByIdHandler.GetTodoById(1);
            var getTodoByIdResult = await _getTodoByIdHandler.Handle(getTodoByIdrequest, CancellationToken.None);

            getTodoByIdResult.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult) getTodoByIdResult;
            objectResult.Value.Should().BeOfType<TodoItemDTO>();
            objectResult.StatusCode.Should().BeNull();
        }

        [Fact]
        public async Task CheckCreateTodoHandler()
        {
            var dto = new TodoItemDTO();
            var request = new CreateTodoHandler.CreateTodo(dto);

            var result = await _createTodoHandler.Handle(request, CancellationToken.None);

            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult) result;
            objectResult.Value.Should().BeOfType<TodoItemDTO>();
            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task CheckPutTodoHandler()
        {
            var createTodoRequest = new CreateTodoHandler.CreateTodo(new TodoItemDTO());
            await _createTodoHandler.Handle(createTodoRequest, CancellationToken.None);

            var updateTodoRequest = new PutTodoHandler.PutTodo(1, new() {Id = 1, Name = "lol"});
            var updateTodoResult = await _putTodoHandler.Handle(updateTodoRequest, CancellationToken.None);

            updateTodoResult.Should().BeOfType<NoContentResult>();

            var getTodoByIdRequest = new GetTodoByIdHandler.GetTodoById(1);
            var getTodoByIdResult = await _getTodoByIdHandler.Handle(getTodoByIdRequest, CancellationToken.None);

            getTodoByIdResult.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult) getTodoByIdResult;
            objectResult.Value.Should().BeOfType<TodoItemDTO>().Which.Name.Should().Be("lol");
            objectResult.StatusCode.Should().BeNull();
        }

        [Fact]
        public async void CheckDeleteTodoHandler()
        {
            var dto = new TodoItemDTO();
            var createTodoRequest = new CreateTodoHandler.CreateTodo(dto);
            await _createTodoHandler.Handle(createTodoRequest, CancellationToken.None);

            var deleteTodoRequest = new DeleteTodoHandler.DeleteTodo(1);
            var deleteTodoResult = await _deleteTodoHandler.Handle(deleteTodoRequest, CancellationToken.None);

            deleteTodoResult.Should().BeOfType<NoContentResult>();

            var getTodoByIdRequest = new GetTodoByIdHandler.GetTodoById(1);
            var getTodoByIdResult = await _getTodoByIdHandler.Handle(getTodoByIdRequest, CancellationToken.None);

            getTodoByIdResult.Should().BeOfType<NotFoundResult>();
        }
    }
}