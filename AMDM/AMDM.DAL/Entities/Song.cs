using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Entities
{
    public class Song : IEntityWithName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SongPageLink { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public string VideoLink { get; set; }
        public virtual ICollection<Chord> Chords { get; set; }

        public int? PerformerId { get; set; }
        public virtual Performer Performer { get; set; }

    }
}
