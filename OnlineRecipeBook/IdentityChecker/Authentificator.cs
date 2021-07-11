using CommonInterfaces;

namespace IdentityChecker
{
    public class Authentificator
    {
        IAuthentificator authentificator;

        public Authentificator(IAuthentificator authRealization) => authentificator = authRealization;

        public bool CheckUserIdentity(string userName, string password) => authentificator.CheckUserIdentity(userName, password);
    }
}
