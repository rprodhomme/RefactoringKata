using WalletKata.Users;

namespace WalletKata.Test.Stubs
{
    class UserSessionNotLoggedInStub : IUserSession
    {
        public User GetLoggedUser()
        {
            return null;
        }
    }
}
