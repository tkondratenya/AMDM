using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMDM.WEB.Models
{
    public class ChordViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<SongViewModel> Songs { get; set; }

        public ChordViewModel()
        {
            Songs = new List<SongViewModel>();
        }
    }
}