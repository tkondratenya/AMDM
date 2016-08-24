using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMDM.WEB.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PerformerViewModel Performer { get; set; }
        public string Text { get; set; }
        public ICollection<ChordViewModel> Chords { get; set; }
        public int Views { get; set; }
        public string VideoLink { get; set; }
    }
}