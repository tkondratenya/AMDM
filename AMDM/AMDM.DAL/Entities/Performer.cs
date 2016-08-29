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
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string BiographyText { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        public Performer()
        {
            Songs = new List<Song>();
        }
    }
}
