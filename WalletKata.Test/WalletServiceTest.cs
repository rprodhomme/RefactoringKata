using NUnit.Framework;
using WalletKata.Users;
using WalletKata.Wallets;
using System.Collections.Generic;
using WalletKata.Test.Stubs;
using WalletKata.Exceptions;
using NSubstitute;

namespace WalletKata.Test
{
    [TestFixture]
    public class WalletServiceTest
    {
        [Test]
        public void TestUserNotLoggedIn()
        {
            Assert.Throws(typeof(UserNotLoggedInException), 
                new TestDelegate(throwsUserNotLoggedIn));
        }

        void throwsUserNotLoggedIn()
        {
            WalletService service = new WalletService(new UserSessionNotLoggedInStub(), null);
            List<Wallet> wallets = service.GetWalletsByUser(new User());
        }

        [Test]
        public void TestUserNotFriend()
        {
            IUserSession userSession = Substitute.For<IUserSession>();
            userSession.GetLoggedUser().Returns(new User());

            WalletService service = new WalletService(userSession, null);
            List<Wallet> wallets = service.GetWalletsByUser(new User());

            Assert.IsNotNull(wallets);
            Assert.AreEqual(0, wallets.Count);
        }

        [Test]
        public void TestSuccess()
        {
            User loggedUser = new User();
            User friend = new User();
            friend.AddFriend(loggedUser);

            List<Wallet> wallets = new List<Wallet>();
            wallets.Add(new Wallet());
            wallets.Add(new Wallet());
            wallets.Add(new Wallet());

            IWalletDAO walletDao = Substitute.For<IWalletDAO>();
            walletDao.FindWalletsByUser(friend).Returns(wallets);

            IUserSession userSession = Substitute.For<IUserSession>();
            userSession.GetLoggedUser().Returns(loggedUser);

            WalletService service = new WalletService(userSession, walletDao);
            List<Wallet> result = service.GetWalletsByUser(friend);

            Assert.IsNotNull(result);
            Assert.AreEqual(wallets.Count, result.Count);
        }
    }
}
