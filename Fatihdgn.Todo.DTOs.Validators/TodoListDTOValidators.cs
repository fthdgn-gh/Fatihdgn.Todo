using FluentValidation;

namespace Fatihdgn.Todo.DTOs.Validators;

public class TodoListCreateDTOValidator : AbstractValidator<TodoListCreateDTO>
{
    public TodoListCreateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class TodoListUpdateDTOValidator : AbstractValidator<TodoListUpdateDTO>
{
    public TodoListUpdateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}