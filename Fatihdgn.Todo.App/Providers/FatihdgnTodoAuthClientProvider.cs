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
    private const string BaseUrl = "http://localhost:32769";
    private static Lazy<IFatihdgnTodoAuthClient> _current = new Lazy<IFatihdgnTodoAuthClient>(() => new FatihdgnTodoAuthClient(BaseUrl, _httpClient));
    public static IFatihdgnTodoAuthClient Current => _current.Value;
}
