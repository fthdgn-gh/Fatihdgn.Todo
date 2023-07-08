namespace Fatihdgn.Todo.App.Managers;


public interface IValueManager<T>
{
    Task<T> GetAsync();
    Task SetAsync(T value);
}
