using System.Collections.Generic;

namespace WalletKata.Users
{
    public class User
    {
        private List<User> friends = new List<User>();

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public bool IsFriendOf(User user)
        {
            foreach (User friend in friends)
            {
                if (friend.Equals(user))
                {
                    return true;
                }
            }
            return false;
        }
    }
}