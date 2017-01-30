using System.Collections.Generic;
using WalletKata.Users;
using WalletKata.Exceptions;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private IUserSession userSession;
        private IWalletDAO walletDao;

        public WalletService(IUserSession userSession, IWalletDAO walletDao)
        {
            this.userSession = userSession;
            this.walletDao = walletDao;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            User loggedUser = userSession.GetLoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }

            if (!user.IsFriendOf(loggedUser))
            {
                return new List<Wallet>();
            }

            return walletDao.FindWalletsByUser(user);
        }
    }
}