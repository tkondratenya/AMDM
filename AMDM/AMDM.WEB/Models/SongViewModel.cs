using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AMDM.WEB.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Ссылка на страницу")]
        public string SongPageLink { get; set; }
        [DisplayName("Текст песни")]
        public string Text { get; set; }
        public int Views { get; set; }
        [DisplayName("Ссылка на видео")]
        public string VideoLink { get; set; }
        public virtual IEnumerable<ChordViewModel> Chords { get; set; }

        public int? PerformerId { get; set; }
        public virtual PerformerViewModel Performer { get; set; }

        public SongViewModel()
        {
            Chords = new List<ChordViewModel>();
        }
    }
}