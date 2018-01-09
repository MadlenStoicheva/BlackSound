using BlackSound.Authentication;
using BlackSound.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                AdminView adminView = new AdminView();
                adminView.Show();
            }
            else
            {
                PlaylistsManagementView playlistManagementView = new PlaylistsManagementView();
                playlistManagementView.Show();
            }
        }
    }
}
