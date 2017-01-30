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
            User loggedUser = RequireLoggedUser();
    
            if (!user.IsFriendOf(loggedUser))
            {
                return new List<Wallet>();
            }

            return walletDao.FindWalletsByUser(user);
        }

        private User RequireLoggedUser()
        {
            User loggedUser = userSession.GetLoggedUser();
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }

            return loggedUser;
        }
    }
}