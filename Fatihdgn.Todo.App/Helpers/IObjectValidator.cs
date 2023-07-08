namespace Fatihdgn.Todo.App.Helpers;

public interface IObjectValidator<T>
{
    string Message { get; set; }
    Func<T, bool> Validate { get; set; }
}
