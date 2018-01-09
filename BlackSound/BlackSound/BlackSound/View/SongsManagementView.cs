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
    public class SongsManagementView
    {
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
                Console.WriteLine("Songs management:");
                Console.WriteLine("[G]et my Songs");
                Console.WriteLine("[V]iew Song");
                Console.WriteLine("[A]dd Song");
                Console.WriteLine("[E]dit Song");
                Console.WriteLine("[D]elete Song");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return SongManagementEnum.Select;
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
            List<Entity.Song> songs = songsRepository.GetAll(AuthenticationService.LoggedUser.Id);

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

        private void View()
        {
            Console.Clear();

            Console.Write("Song ID: ");
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

            Console.WriteLine("ID:" + song.Id);
            Console.WriteLine("Title :" + song.Title);
            Console.WriteLine("Artist Name :" + song.ArtistName);
            Console.WriteLine("Year :" + song.Year + "y");

            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Entity.Song song = new Entity.Song();
            song.ParentUserId = AuthenticationService.LoggedUser.Id;

            Console.WriteLine("Add new Song:");
            Console.Write("Title: ");
            song.Title = Console.ReadLine();
            Console.Write("Atist Name: ");
            song.ArtistName = Console.ReadLine();
            Console.Write("Year :");
            song.Year = Console.ReadLine();
                      

            SongsRepository songRepository = new SongsRepository("songs.txt");
            songRepository.Save(song);

            Console.WriteLine("Songs saved successfully.");
            Console.ReadKey(true);

            Console.WriteLine("-------------------------------------------");

        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Song ID: ");
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

            Console.WriteLine("Editing Song (" + song.Title + ")");
            Console.WriteLine("ID:" + song.Id);

            Console.WriteLine("Title :" + song.Title);
            Console.Write("New Title:");
            string Title = Console.ReadLine();

            Console.WriteLine("Artist Name :" + song.ArtistName);
            Console.Write("New Artist Name :");
            string ArtistName = Console.ReadLine();

            Console.WriteLine("Year :" + song.Year);
            Console.Write("New Year :");
            string Year = Console.ReadLine();


            if (!string.IsNullOrEmpty(Title))
                song.Title = Title;
            if (!string.IsNullOrEmpty(ArtistName))
                song.ArtistName = ArtistName;
            if (!string.IsNullOrEmpty(Year))
                song.Year = Year;

            songsRepository.Save(song);

            Console.WriteLine("Song saved successfully.");
            Console.ReadKey(true);

        }

        private void Delete()
        {
            SongsRepository songsRepository = new SongsRepository("songs.txt");

            Console.Clear();

            Console.WriteLine("Delete Task:");
            Console.Write("Task Id: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            Entity.Song song = songsRepository.GetById(taskId);
            if (song == null)
            {
                Console.WriteLine("Song not found!");
            }
            else
            {
                songsRepository.Delete(song);
                Console.WriteLine("Song deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
