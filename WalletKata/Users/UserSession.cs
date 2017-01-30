using WalletKata.Exceptions;

namespace WalletKata.Users
{
    public class UserSession : IUserSession
    {
        private static readonly IUserSession userSession = new UserSession();

        private UserSession() { }

        public static IUserSession GetInstance()
        {
            return userSession;
        }

        public User GetLoggedUser()
        {
            throw new ThisIsAStubException("UserSession.IsUserLoggedIn() should not be called in an unit test");
        }
    }
}