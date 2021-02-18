using FluentValidation;

namespace ToDoList.Core.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        
        public class ToDoItemValidator : AbstractValidator<TodoItemDTO>
        {
            public ToDoItemValidator()
            {
                RuleFor(todoitemdto => todoitemdto.Name).NotNull().MaximumLength(500);
                RuleFor(todoitemdto => todoitemdto.Id).GreaterThan(0);
            }
        }
    }
}