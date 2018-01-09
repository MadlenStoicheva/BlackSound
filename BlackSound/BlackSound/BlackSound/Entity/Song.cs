using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Entity
{
    public class Song
    {
        public int Id { get; set; }
        public int ParentUserId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string Year { get; set; }
    }
}
