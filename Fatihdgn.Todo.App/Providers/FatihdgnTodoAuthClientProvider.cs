using Fatihdgn.Todo.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.App.Providers;

public class FatihdgnTodoAuthClientProvider
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static Lazy<IFatihdgnTodoAuthClient> _current = new Lazy<IFatihdgnTodoAuthClient>(() => new FatihdgnTodoAuthClient(Constants.ApiBaseUrl, _httpClient));
    public static IFatihdgnTodoAuthClient Current => _current.Value;
    public static HttpClient HttpClient => _httpClient;
}
