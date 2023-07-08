namespace Fatihdgn.Todo.App.Helpers;

public interface IValidatableObject<T>
{
    T Value { get; set; }
    bool HasMessage { get; }
    string Message { get; set; }
    ICollection<IObjectValidator<T>> Validators { get; set; }
}
