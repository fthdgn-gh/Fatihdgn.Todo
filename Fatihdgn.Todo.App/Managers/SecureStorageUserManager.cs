namespace Fatihdgn.Todo.App.Managers;

public class SecureStorageUserManager : UserManager
{
    private static Lazy<SecureStorageUserManager> instance = new Lazy<SecureStorageUserManager>();
    public static SecureStorageUserManager Instance => instance.Value;
    public SecureStorageUserManager() : base(new SecureStorageValueManager("Email"), new SecureStorageValueManager("AccessToken"), new SecureStorageValueManager("RefreshToken")) { }

}
