using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Entity
{
    public class Playlist
    {
        public int Id { get; set; }
        public int ParentUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Songs { get; set; }
        public bool IsPublic { get; set; }
        public string performer{ get; set; }
    }
}
