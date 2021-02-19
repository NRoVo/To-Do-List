using Xunit;
using ToDoList.Core.Models;
using FluentValidation.TestHelper;

namespace ToDoList.UnitTests
{
    public class ValidationTests

    {
        private readonly TodoItemDTO.ToDoItemValidator _validator = new();

        private readonly string _name =
            "йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхйцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхйцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхйцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхйцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхйцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзхъфывапролджэячсмитьбю.йцукенгшщзх";

        [Fact]
        public void CheckForNull()
        {
            var model = new TodoItemDTO(){Name = null};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(dto => dto.Name);
        }

        [Fact]
        public void CheckIfIdIsCorrect()
        {
            var model = new TodoItemDTO() {Id = 0};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(dto => dto.Id);
        }

        [Fact]
        public void CheckNameLength()
        {
            var model = new TodoItemDTO() {Name = _name};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(dto => dto.Name);
        }
    }
}
