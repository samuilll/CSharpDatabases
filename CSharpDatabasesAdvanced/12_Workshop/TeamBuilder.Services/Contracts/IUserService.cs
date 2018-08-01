using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services.Contracts
{
    public interface IUserService
    {
        TModel RegisterUser<TModel>(params string[] args);

        void Login(string username, string password);

        string Logout();

        string DeleteUser();
    }
}
