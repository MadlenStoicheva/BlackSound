using BlackSound.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    public class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Administration View:");
                    Console.WriteLine("[S]ongs Management");
                    Console.WriteLine("[U]ser Management");
                    Console.WriteLine("E[x]it");

                    string choice = Console.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "S":
                            {
                                SongsManagementView view = new SongsManagementView();
                                view.Show();

                                break;
                            }
                        case "U":
                            {
                                UserManagementView view = new UserManagementView();
                                view.Show();

                                break;
                            }
                        case "X":
                            {
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice.");
                                Console.ReadKey(true);
                                break;
                            }
                    }
                }
            }
        }
    }
}
