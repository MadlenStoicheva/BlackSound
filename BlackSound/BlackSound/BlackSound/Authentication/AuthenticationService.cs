using BlackSound.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Authentication
{
    public static class AuthenticationService
    {
        public static Entity.User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UserRepository userRepo = new UserRepository("users.txt");
            AuthenticationService.LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
