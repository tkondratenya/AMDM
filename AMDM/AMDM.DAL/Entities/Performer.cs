using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Entities
{
    public class Performer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
        public byte[] ImageData { get; set; }
        public string FounderName { get; set; }
        public string BiographyText { get; set; }
    }
}
