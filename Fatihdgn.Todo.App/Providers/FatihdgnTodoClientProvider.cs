using Fatihdgn.Todo.API.Client;

namespace Fatihdgn.Todo.App.Providers;

public class FatihdgnTodoClientProvider
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static Lazy<IFatihdgnTodoClient> _current = new Lazy<IFatihdgnTodoClient>(() => new FatihdgnTodoClient(ProviderConstants.FatihdgnTodoClientBaseUrl, _httpClient));
    public static IFatihdgnTodoClient Current => _current.Value;
}
