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
        public ICollection<SongViewModel> Songs { get; set; }
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string FounderName { get; set; }
        public string BiographyText { get; set; }
    }
}