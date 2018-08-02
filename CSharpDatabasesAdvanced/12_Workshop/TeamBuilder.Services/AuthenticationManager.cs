using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Services
{
   public static class AuthenticationManager
    {
        public static User User { get; set; }

        public static void Login(User user)
        {
            User = user;
        }// – saves given user as logged user until logout or exit of the application 
        public static void Logout()
        {
            User = null;
        } //– logs out currently logged in user, if there is none should throw exception(use the method below)
        public static void Authorize()
        {
            throw new ArgumentException();

        } //– throws InvalidOperationException if there is no logged in user
        public static bool IsAuthenticated()
        {
            return User != null;
        }// – returns true if there is logged in user else returns false
        public static User GetCurrentUser(TeamBuilderContext context)
        {

            return context.Users.FirstOrDefault(u=>u.Id == User.Id);
        }// – gets currently logged in user if there is not throws exception
    }
}
