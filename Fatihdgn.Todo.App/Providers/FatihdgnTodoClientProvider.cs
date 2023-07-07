using Fatihdgn.Todo.API.Client;

namespace Fatihdgn.Todo.App.Providers;

public class FatihdgnTodoClientProvider
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "http://localhost:32769";
    private static Lazy<IFatihdgnTodoClient> _current = new Lazy<IFatihdgnTodoClient>(() => new FatihdgnTodoClient(BaseUrl, _httpClient));
    public static IFatihdgnTodoClient Current => _current.Value;
}
