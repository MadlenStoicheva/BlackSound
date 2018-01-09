using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Repository
{
    public class PlaylistRepository
    {
        private readonly string filePath;

        public PlaylistRepository(string filePath)
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
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();

                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (id <= playlist.Id)
                    {
                        id = playlist.Id + 1;
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

        private void Insert(Entity.Playlist item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentUserId);
                sw.WriteLine(item.Name);
                sw.WriteLine(item.Description);
                sw.WriteLine(item.Songs);
                sw.WriteLine(item.IsPublic);

            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Entity.Playlist item)
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
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (playlist.Id != item.Id)
                    {
                        sw.WriteLine(playlist.Id);
                        sw.WriteLine(playlist.ParentUserId);
                        sw.WriteLine(playlist.Name);
                        sw.WriteLine(playlist.Description);
                        sw.WriteLine(playlist.Songs);
                        sw.WriteLine(playlist.IsPublic);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentUserId);
                        sw.WriteLine(item.Name);
                        sw.WriteLine(item.Description);
                        sw.WriteLine(item.Songs);
                        sw.WriteLine(item.IsPublic);

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

        public Entity.Playlist GetById(int Id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (playlist.Id == Id)
                    {
                        return playlist;
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
        public List<Entity.Playlist> GetAll()
        {
            List<Entity.Playlist> result = new List<Entity.Playlist>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    result.Add(playlist);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }
        public List<Entity.Playlist> GetAll(int ParentUserId)
        {
            List<Entity.Playlist> result = new List<Entity.Playlist>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (playlist.ParentUserId == ParentUserId)
                    {
                        result.Add(playlist);
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public List<Entity.Playlist> ShowMy(string performer)
        {
            List<Entity.Playlist> result = new List<Entity.Playlist>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (playlist.performer == performer)
                    {
                        result.Add(playlist);
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(Entity.Playlist item)
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
                    Entity.Playlist playlist = new Entity.Playlist();
                    playlist.Id = Convert.ToInt32(sr.ReadLine());
                    playlist.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    playlist.Name = sr.ReadLine();
                    playlist.Description = sr.ReadLine();
                    playlist.Songs = sr.ReadLine();
                    playlist.IsPublic = Convert.ToBoolean(sr.ReadLine());

                    if (playlist.Id != item.Id)
                    {
                        sw.WriteLine(playlist.Id);
                        sw.WriteLine(playlist.ParentUserId);
                        sw.WriteLine(playlist.Name);
                        sw.WriteLine(playlist.Description);
                        sw.WriteLine(playlist.Songs);
                        sw.WriteLine(playlist.IsPublic);
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

        public void Save(Entity.Playlist item)
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

