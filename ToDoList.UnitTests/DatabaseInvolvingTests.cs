using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Db;

namespace ToDoList.UnitTests
{
    public abstract class DatabaseInvolvingTests
    {
        protected static TodoContext DbContextCreate()
        {
            var context = new TodoContext(new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}