using Fatihdgn.Todo.API.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Fatihdgn.Todo.API.Tests
{
    public class UnitTest1
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private readonly FatihdgnTodoClient _todoClient;
        public UnitTest1()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing") // Use a specific environment (e.g., "Testing") to configure your Web API differently for tests
                .UseStartup<API.Program>()); // Replace with the actual Startup class of your Web API

            // Set up the HttpClient instance
            _client = _server.CreateClient();
            _todoClient = new FatihdgnTodoClient("", _client);
        }
        [Fact]
        public void Test1()
        {

        }
    }
}