using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMDM.WEB.Models
{
    public class PerformerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string BiographyText { get; set; }
        public virtual ICollection<SongViewModel> Songs { get; set; }

        public PerformerViewModel()
        {
            Songs = new List<SongViewModel>();
        }
    }
}