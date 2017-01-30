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

            if (loggedUser != null)
            {
                if (user.IsFriendOf(loggedUser))
                {
                    return walletDao.FindWalletsByUser(user);
                }

                return new List<Wallet>();
            }
            else
            {
                throw new UserNotLoggedInException();
            }      
        }         
    }
}