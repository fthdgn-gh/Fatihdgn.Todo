using Fatihdgn.Todo.API.Client;

namespace Fatihdgn.Todo.App.Providers;

public class FatihdgnTodoClientProvider
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static Lazy<IFatihdgnTodoClient> _current = new Lazy<IFatihdgnTodoClient>(() => new FatihdgnTodoClient(Constants.ApiBaseUrl, _httpClient));
    public static IFatihdgnTodoClient Current => _current.Value;
    public static HttpClient HttpClient => _httpClient;

    private static string _accessToken;
    public static void ApplyAccessToken(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

}