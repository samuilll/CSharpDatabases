using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services
{
   public class ExeptionMessageHandler
    {
        public const string PassworsNotMatchExeption = "Passwords do not match!";

        public const string UsernameAlreadyExistExeption = "Username {0} is already taken!";

        public const string EmailAlreadyExistExeption = "Email {0} is already taken!";

        public const string TownAlreadyAddedExeption = "Town {0} was already added!";

        public const string UserDoesNotExistExeption = "User {0} not found!";

        public const string PropertyNotSupportedExeption = "Property {0} not supported!";

        public const string TownNotFoundExeption = "Town {0} not found!";

        public const string WrongValueExeption = "Value {0} not valid! {1}{2}";

        public const string AlreadyDeletedExeption = "User {0} is already deleted!";

        public const string TagAlreadyExistExeption = "Tag {0} exist!";

        public const string TagDoesNotExist = "Tag {0} does not exist!";

        public const string InvalidTags = "Invalid Tags";

        public const string ColorNotFount = "Color {0} not found!";

        public const string AlbumAlreadyExistExeption = "Album {0} does exist!";

        public const string AlbumOrTagDoesNotExisExeption  = "Either tag or album do not exist!";

        public const string AlreadyFriendsExeption = "{0} is already a friend to {1}";

        public const string NoSuchRequestExeption = "{0} has not added {1} as a friend";

        public const string AlbumDoesNotExistExeption = "Album with {0} does not exist";

        public const string PermissionExeption = "Permission must be either “Owner” or “Viewer”!";

        public const string commandNotValidExeption = "Command {0} not valid!";

        public const string InvalidUsernameOrPassowordExeption = "Invalid username or password!";

        public const string FirstLogOutExeption = "You should log out first!";

        public const string FirstLogInExeption = "You should log in first in order to logout";

        public const string InvalidCredentialsExeption = "InvalidCredentials";


    }
}
