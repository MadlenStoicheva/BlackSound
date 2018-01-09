using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Repository
{
    public class SongsRepository
    {
        private readonly string filePath;

        public SongsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        private int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 1;
            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    if (id <= song.Id)
                    {
                        id = song.Id + 1;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(Entity.Song item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentUserId);
                sw.WriteLine(item.Title);
                sw.WriteLine(item.ArtistName);
                sw.WriteLine(item.Year);

            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Entity.Song item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    if (song.Id != item.Id)
                    {
                        sw.WriteLine(song.Id);
                        sw.WriteLine(song.ParentUserId);
                        sw.WriteLine(song.Title);
                        sw.WriteLine(song.ArtistName);
                        sw.WriteLine(song.Year);

                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentUserId);
                        sw.WriteLine(item.Title);
                        sw.WriteLine(item.ArtistName);
                        sw.WriteLine(item.Year);

                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public Entity.Song GetById(int Id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    if (song.Id == Id)
                    {
                        return song;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }
        public List<Entity.Song> GetAll()
        {
            List<Entity.Song> result = new List<Entity.Song>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    result.Add(song);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }
        public List<Entity.Song> GetAll(int ParentUserId)
        {
            List<Entity.Song> songs = new List<Entity.Song>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    if (song.ParentUserId == ParentUserId)
                    {
                        songs.Add(song);
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return songs;
        }


        public void Delete(Entity.Song item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Song song = new Entity.Song();
                    song.Id = Convert.ToInt32(sr.ReadLine());
                    song.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    song.Title = sr.ReadLine();
                    song.ArtistName = sr.ReadLine();
                    song.Year = sr.ReadLine();

                    if (song.Id != item.Id)
                    {
                        sw.WriteLine(song.Id);
                        sw.WriteLine(song.ParentUserId);
                        sw.WriteLine(song.Title);
                        sw.WriteLine(song.ArtistName);
                        sw.WriteLine(song.Year);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(Entity.Song item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}

