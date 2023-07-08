namespace Fatihdgn.Todo.App.Managers;

public class SecureStorageValueManager : IValueManager<string>
{
    private readonly string _name;
    public SecureStorageValueManager(string name)
    {
        _name = name;
    }

    public async Task<string> GetAsync() => await SecureStorage.GetAsync(_name);
    public async Task SetAsync(string value) => await SecureStorage.SetAsync(_name, value);
}
