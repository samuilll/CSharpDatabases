using PhotoShare.Models;
using PhotoShare.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core
{
   public static class Session
    {
        public static User User { get; set; }

        public static bool HasCredentials(string username)
        {
            var hasCredentials = User.Username == username;

            return hasCredentials;
        }
        public static bool HasLoggedUser()
        {       
            return User!=null;
        }

    }
}
