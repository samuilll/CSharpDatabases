using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services
{
    using AutoMapper;
    using Contracts;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class UserService : IUserService
    {
        private IMapper mapper;

        public UserService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Login(string username, string password)
        {
            var user = CorrectUser(username, password);

            if (user == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserOrPasswordIsInvalid));
            }

            ShouldLogOut();

            AuthenticationManager.Login(user);
        }

        private static void ShouldLogOut()
        {
            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.LogoutFirst));
            }
        }

        private User CorrectUser(string username, string password)
        {
            using (var db = new TeamBuilderContext())
            {
                if (db.Users.SingleOrDefault(u => u.Username == username) == null)
                {
                    return null;
                }

                var user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user.Password.CompareTo(password)!=0 || user.IsDeleted==true)
                {
                    return null;
                }

                return user;
            }
        }

        public TModel RegisterUser<TModel>(params string[] args)
        {
            var username = args[0];

            if (!Validation.IsStringValid(username, Constants.MinUsernameLength, Constants.MaxUsernameLength))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            var password = args[1];

            if (!Validation.IsStringValid(password, Constants.MinPasswordLength, Constants.MaxPasswordLength))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            var repeatedPassword = args[2];

            var firstName = args[3];

            var lastName = args[4];

            var successConvertAge = int.TryParse(args[5],out int age);

            if (!successConvertAge || age<=0)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.AgeNotValid));
            }

            var successConvertEnum = Enum.TryParse<Gender>(args[6], out Gender gender );

            if (!successConvertEnum)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.GenderNotValid));
            }

            if (password.CompareTo(repeatedPassword)!=0)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.PasswordDoesNotMatch));
            }

            if (IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken,username));
            }

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.LogoutFirst));
            }

            var user = new User()
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Age = age,
                Gender = gender
            };

            var userDto = this.mapper.Map<TModel>(user);

            using (var db =new TeamBuilderContext())
            {
                db.Users.Add(user);

                db.SaveChanges();
            }

            return userDto;
        }

        private bool IsUserExisting(string username)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Users.SingleOrDefault(u => u.Username == username) != null;
            }
        }

        public string Logout()
        {
            ShouldLogIn();

            var username = AuthenticationManager.GetCurrentUser(new TeamBuilderContext()).Username;

            AuthenticationManager.Logout();

            return username;
        }

        private static void ShouldLogIn()
        {
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public string DeleteUser()
        {
            ShouldLogIn();
            using (var db = new TeamBuilderContext())
            {
             var user = AuthenticationManager.GetCurrentUser(db);

            AuthenticationManager.Logout();

           
                var userToDelete = db.Users.Find(user.Id);

                userToDelete.IsDeleted = true;

                db.SaveChanges();

                return user.Username;
            }

        }
    }
}
//RegisterUser<username> <password> <repeat-password> <firstName> <lastName> <age> <gender>