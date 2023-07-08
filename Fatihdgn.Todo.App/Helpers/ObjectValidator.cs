namespace Fatihdgn.Todo.App.Helpers;

public record struct ObjectValidator<T>(string Message, Func<T, bool> Validate) : IObjectValidator<T>;
