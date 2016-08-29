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
        public string SongPageLink { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public string VideoLink { get; set; }
        public virtual ICollection<ChordViewModel> Chords { get; set; }

        public int? PerformerId { get; set; }
        public virtual PerformerViewModel Performer { get; set; }

        public SongViewModel()
        {
            Chords = new List<ChordViewModel>();
        }
    }
}