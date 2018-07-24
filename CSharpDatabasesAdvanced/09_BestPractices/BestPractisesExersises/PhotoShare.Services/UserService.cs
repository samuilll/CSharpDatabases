using System;
using Microsoft.EntityFrameworkCore;
namespace PhotoShare.Services
{
    using Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Models.Attributes;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class UserService : IUserService
    {
        private const string NoFriendsMessage = "No friends for this user. :(";
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public User Create(params string[] arguments)
        {
            string username = arguments[0];
            string password = arguments[1];
            string repeatPassword = arguments[2];
            string email = arguments[3];

            var users = context.Users.ToList();

            if (users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.UsernameAlreadyExistExeption, username));
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException(ExeptionMessageHandler.PassworsNotMatchExeption);
            }

            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };
        
            if (users.Any(u => u.Email == email))
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.EmailAlreadyExistExeption, email));
            }

            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            var user = this.GetByUsername(username);

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.AlreadyDeletedExeption, username));
            }

            user.IsDeleted = true;

            this.context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            User user;

            user = context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.UserDoesNotExistExeption, "Id:1"));
            }

            return user;
        }
        public User GetByUsername(string username)
        {
            User user;

            user = context.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.UserDoesNotExistExeption, username));
            }

            return user;
        }


        public User Modify(string[] data)
        {

            var username = data[0];
            var propertyName = data[1];
            string newValueAsString = data[2];

            var user = this.GetByUsername(username);


            if (propertyName != "Password" && propertyName != "BornTown" && propertyName != "CurrentTown")
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.PropertyNotSupportedExeption, propertyName));
            }


            try
            {
                switch (propertyName)
                {
                    case "BornTown":
                        {
                            ChangeBornTown(newValueAsString, user);

                            break;
                        }
                    case "CurrentTown":
                        {
                            ChangeCurrentTown(newValueAsString, user);

                            break;
                        }
                    case "Password":
                        {
                            ChangePassword(newValueAsString, user);

                            break;
                        }
                    default:
                        break;
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format(ExeptionMessageHandler.WrongValueExeption, newValueAsString, Environment.NewLine, e.Message));
            }


            return user;
        }

        private void ChangePassword(string newValueAsString, User user)
        {
            user.Password = newValueAsString;

        }

        private void ChangeBornTown(string newValueAsString, User user)
        {
            var town = context.Towns.SingleOrDefault(t => t.Name == newValueAsString);

            if (town == null)
            {
                throw new ArgumentException(String.Format(ExeptionMessageHandler.TownNotFoundExeption, newValueAsString));
            }

            user.BornTown = town;

        }
        private void ChangeCurrentTown(string newValueAsString, User user)
        {
            var town = context.Towns.SingleOrDefault(t => t.Name == newValueAsString);

            if (town == null)
            {
                throw new ArgumentException(String.Format(ExeptionMessageHandler.TownNotFoundExeption, newValueAsString));
            }

            user.CurrentTown = town;

        }

        public void AddFriend(string username1, string username2)
        {
            var requestingUser = this.GetByUsername(username1);

            var addedFriend = this.GetByUsername(username2);

            context.Entry(requestingUser).Collection(ru => ru.FriendsAdded).Load();

            var friends = requestingUser.FriendsAdded.ToList();

            if (friends.Any(f => f.Friend.Username == username2))
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.AlreadyFriendsExeption, username2, username1));
            }

            requestingUser.FriendsAdded
                .Add(new Friendship()
                {
                    User = requestingUser,
                    Friend = addedFriend
                });

            context.SaveChanges();
        }

        public void AcceptFriend(string username1, string username2)
        {
            var requestingUser = this.GetByUsername(username2);

            var acceptingUser = this.GetByUsername(username1);

            context.Entry(requestingUser).Collection(ru => ru.FriendsAdded).Load();
            context.Entry(acceptingUser).Collection(au => au.FriendsAdded).Load();

            var requestingUserFriends = requestingUser.FriendsAdded.ToList();

            var acceptingUserFriends = acceptingUser.FriendsAdded.ToList();


            if (!requestingUserFriends.Any(f => f.Friend.Username == username1))
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.NoSuchRequestExeption, username2, username1));
            }

            if (acceptingUserFriends.Any(f => f.Friend.Username == username2))
            {
                throw new InvalidOperationException(string.Format(ExeptionMessageHandler.AlreadyFriendsExeption, username2, username1));
            }

            acceptingUser.FriendsAdded
                .Add(new Friendship()
                {
                    User = acceptingUser,
                    Friend = requestingUser
                });

            context.SaveChanges();
        }

        public string ListFriends(string username)
        {
            var user = this.GetByUsername(username);

            var sb = new StringBuilder();

            context.Entry(user).Collection(u => u.FriendsAdded).Load();
            context.Entry(user).Collection(u => u.AddedAsFriendBy).Load();

            var friendsAdded = user.FriendsAdded.Select(f=>f.FriendId).ToList();
            var addedAsFrinedBy = user.AddedAsFriendBy.Select(f=>f.UserId).ToList();

            var realFriendsIds = friendsAdded.Where(Id => addedAsFrinedBy.Contains(Id));

            var realFriends = user.FriendsAdded.Where(f => realFriendsIds.Contains(f.FriendId)).ToList();

            if (realFriends.Count == 0)
            {
                sb.AppendLine(NoFriendsMessage);

                return sb.ToString().TrimEnd('\r', '\n'); 
            }

            sb.AppendLine("Friends");

 
            foreach (var friendId in realFriends.Select(f=>f.FriendId))
            {
                var friend = GetById(friendId);

                sb.AppendLine($"-{friend.Username}");
            }

            return sb.ToString().TrimEnd('\r', '\n');

        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            var user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user==null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.InvalidUsernameOrPassowordExeption));
            }

            return user;
        }
    }
}
