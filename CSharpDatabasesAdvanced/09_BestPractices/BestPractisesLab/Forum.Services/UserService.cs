using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Contracts;
using System.Linq;

namespace Forum.Services
{
    public class UserService : IUserService
    {
        private readonly ForumDbContext context;

        public UserService(ForumDbContext context)
        {
            this.context = context;
        }

        public User ById(int id)
        {
            var user = context.Users.Find(id);

            return user;
        }

        public User ByUsername(string username)
        {
            var user = context.Users.SingleOrDefault(u=>u.Username==username);

            return user;
        }

        public User ByUsernameAndPassword(string username, string password)
        {
            var user = context.Users.SingleOrDefault(u => u.Username == username && u.Password==password);

            return user;
        }

        public User Create(string username, string password)
        {
            var user = new User(username, password);

            var alreadyExist = context.Users.FirstOrDefault(u => u.Username == username) != null;

            if (alreadyExist)
            {
                return null;
            }

            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = this.ById(id);

            context.Remove(user);

            context.SaveChanges();
        }
    }
}
