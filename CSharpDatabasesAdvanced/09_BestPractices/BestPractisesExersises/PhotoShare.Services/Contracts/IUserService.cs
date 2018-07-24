using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services.Contracts
{
   public interface IUserService
    {
        User GetById(int id);

        User GetByUsername(string userName);

        User GetByEmail(string email);

        User Create( string[] arguments);

        void Delete(string username);

        User Modify(string[] arguments);

        void AddFriend(string username1, string username2);

        void AcceptFriend(string username1, string username2);

        User GetByUsernameAndPassword(string username, string password);

        string ListFriends(string username);
    }
}
