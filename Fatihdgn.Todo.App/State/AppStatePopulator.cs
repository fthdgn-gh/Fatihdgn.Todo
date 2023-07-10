using Fatihdgn.Todo.App.Managers;
using Fatihdgn.Todo.App.Providers;

namespace Fatihdgn.Todo.App.State;

public static class AppStatePopulator
{
    public static async Task<AppState> Populate(AppState state)
    {
        var userManager = SecureStorageUserManager.Instance;

        state.User.Email = await userManager.Email.GetAsync();
        state.User.AccessToken = await userManager.AccessToken.GetAsync();
        state.User.RefreshToken = await userManager.RefreshToken.GetAsync();

        var todoClient = FatihdgnTodoClientProvider.Current;
        state.Lists.Clear();
        foreach(var list in await todoClient.GetAllListsAsync())
            state.Lists.Add(list);

        var currentList = state.Lists.FirstOrDefault();
        state.Items.Clear();
        if(currentList is not null)
        {
            state.CurrentTodoListId = currentList.Id;
            state.CurrentTodoList.MapFrom(currentList);
            foreach(var item in await todoClient.GetAllItemsByListIdAsync(currentList.Id))
                state.Items.Add(item);
        }


        state.Templates.Clear();
        foreach (var template in await todoClient.GetAllTemplatesAsync())
            state.Templates.Add(template);

        return state;
    }
}
