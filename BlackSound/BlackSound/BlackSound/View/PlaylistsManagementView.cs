using BlackSound.Authentication;
using BlackSound.Repository;
using BlackSound.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.View
{
    public class PlaylistsManagementView
    {
        //private Entity.Playlist playlist = null;

        public void Show()
        {
            while (true)
            {
                SongManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case SongManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case SongManagementEnum.ShowMy:
                            {
                                ShowMy();
                                break;
                            }
                        case SongManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case SongManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case SongManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case SongManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case SongManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        private SongManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Plylist management:");
                Console.WriteLine("[G]et All Songs");
                Console.WriteLine("[S]how my Playlist");
                Console.WriteLine("[V]iew Playlist");
                Console.WriteLine("[A]dd Playlist");
                Console.WriteLine("[E]dit Playlist");
                Console.WriteLine("[D]elete Playlist");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return SongManagementEnum.Select;
                        }
                    case "S":
                        {
                            return SongManagementEnum.ShowMy;
                        }
                    case "V":
                        {
                            return SongManagementEnum.View;
                        }
                    case "A":
                        {
                            return SongManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return SongManagementEnum.Update;
                        }
                    case "D":
                        {
                            return SongManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return SongManagementEnum.Exit;
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
        private void GetAll()
        {
            Console.Clear();

            SongsRepository songsRepository = new SongsRepository("songs.txt");
            List<Entity.Song> songs = songsRepository.GetAll();

            foreach (Entity.Song song in songs)
            {
                Console.WriteLine("ID:" + song.Id);
                Console.WriteLine("Title :" + song.Title);
                Console.WriteLine("Artist Name :" + song.ArtistName);
                Console.WriteLine("Year :" + song.Year + "y");

                Console.WriteLine("------------------------------------");
            }

            Console.ReadKey(true);
        }

        private void ShowMy()
        {
            Console.Clear();

            PlaylistRepository playlistRepository = new PlaylistRepository("playlist.txt");
            List<Entity.Playlist> playlists = playlistRepository.GetAll(AuthenticationService.LoggedUser.Id);

            foreach (Entity.Playlist playlist in playlists)
            {
                Console.WriteLine("ID:" + playlist.Id);
                Console.WriteLine("Name :" + playlist.Name);
                Console.WriteLine("Description :" + playlist.Description);
                Console.WriteLine("Songs :" + playlist.Songs);
                Console.WriteLine("IsPublic :" + playlist.IsPublic);

                Console.WriteLine("------------------------------------");
                             
            }
            if (playlists.Count == 0)
            {
                Console.WriteLine("You don't have any playlists yet!");
            }

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            Console.Write("Playlist ID: ");
            int playlistId = Convert.ToInt32(Console.ReadLine());
            PlaylistRepository playlistRepository = new PlaylistRepository("playlist.txt");
            Entity.Playlist playlist = playlistRepository.GetById(playlistId);
            int lognat = AuthenticationService.LoggedUser.Id;


            if (playlist == null)
            {
                Console.Clear();
                Console.WriteLine("Playlist not found.");
                Console.ReadKey(true);
                return;
            }
            if (playlist.IsPublic == true || playlist.ParentUserId == lognat)
            {
                Console.WriteLine("ID:" + playlist.Id);
                Console.WriteLine("Name :" + playlist.Name);
                Console.WriteLine("Description :" + playlist.Description);
                Console.WriteLine("Songs :" + playlist.Songs);
                Console.WriteLine("IsPublic :" + playlist.IsPublic);

                Console.WriteLine("------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("This playlist is not public!");
            }

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Entity.Playlist playlist = new Entity.Playlist();
            playlist.ParentUserId = AuthenticationService.LoggedUser.Id;
            PlaylistRepository playlistRepository = new PlaylistRepository("playlist.txt");

            Console.WriteLine("Add new Playlist:");
            Console.Write("Name: ");
            playlist.Name = Console.ReadLine();
            Console.Write("Description: ");
            playlist.Description = Console.ReadLine();
            Console.Write("Add Song by Song ID :");
            int songId = Convert.ToInt32(Console.ReadLine());

            SongsRepository songsRepository = new SongsRepository("songs.txt");
            Entity.Song song = songsRepository.GetById(songId);
            if (song == null)
            {
                Console.Clear();
                Console.WriteLine("Song not found.");
                Console.ReadKey(true);
                return;
            }
            playlist.Songs = song.Title + ", " + song.ArtistName + ", " + song.Year + " y.";

            Console.Write("IsPublic: ");
            playlist.IsPublic = Convert.ToBoolean(Console.ReadLine());

            playlistRepository.Save(playlist);

            Console.WriteLine("Playlist saved successfully.");
            Console.ReadKey(true);

            Console.WriteLine("-------------------------------------------");

        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Playlist ID: ");
            int playlistId = Convert.ToInt32(Console.ReadLine());

            PlaylistRepository playlistRepository = new PlaylistRepository("playlist.txt");
            Entity.Playlist playlist = playlistRepository.GetById(playlistId);


            if (playlist == null)
            {
                Console.Clear();
                Console.WriteLine("Playlist not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing Playlist (" + playlist.Name + ")");
            Console.WriteLine("ID:" + playlist.Id);

            Console.WriteLine("Name :" + playlist.Name);
            Console.Write("New Name:");
            string Name = Console.ReadLine();

            Console.WriteLine("Description :" + playlist.Description);
            Console.Write("New Description :");
            string Description = Console.ReadLine();

            Console.WriteLine("IsPublic :" + playlist.IsPublic);
            Console.Write("New IsPublic :");
            string IsPublic = Console.ReadLine();


            Console.WriteLine("Add or Remove songs ?");
            string answer = Console.ReadLine();
            if(answer.ToLower() == "add")
            {
                Console.Write("Add Song by Song ID :");
                int songId = Convert.ToInt32(Console.ReadLine());
    
                SongsRepository songsRepository = new SongsRepository("songs.txt");
                Entity.Song song = songsRepository.GetById(songId);
                if (song == null)
                {
                    Console.Clear();
                    Console.WriteLine("Song not found.");
                    Console.ReadKey(true);
                    return;
                }
                playlist.Songs += "  /  " + song.Title + ", " + song.ArtistName + ", " + song.Year + " y.";
                Console.WriteLine("You add the song : " + song.Title + ", " + song.ArtistName + ", " + song.Year + " y. to your playlist!" );

            }
            else if(answer.ToLower()=="remove"){
                Console.Write("Remove Song by Song Index in Playlist :");
                int songId = Convert.ToInt32(Console.ReadLine());
                //playlist.Songs;

            }
            

            if (!string.IsNullOrEmpty(Name))
                playlist.Name = Name;
            if (!string.IsNullOrEmpty(Description))
                playlist.Description = Description;

            playlistRepository.Save(playlist);

            Console.WriteLine("Playlist saved successfully.");
            Console.ReadKey(true);

        }

        private void Delete()
        {
            PlaylistRepository playlistRepository = new PlaylistRepository("playlist.txt");

            Console.Clear();

            Console.WriteLine("Delete Playlist:");
            Console.Write("Playlist Id: ");
            int playlistId = Convert.ToInt32(Console.ReadLine());

            Entity.Playlist playlist = playlistRepository.GetById(playlistId);
            if (playlist == null)
            {
                Console.WriteLine("Plylist not found!");
            }
            else
            {
                playlistRepository.Delete(playlist);
                Console.WriteLine("Playlist deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
