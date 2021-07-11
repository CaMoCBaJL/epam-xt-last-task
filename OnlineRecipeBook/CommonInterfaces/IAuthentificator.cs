namespace CommonInterfaces
{
    public interface IAuthentificator
    {
        bool CheckUserIdentity(string userName, string password);
    }
}

