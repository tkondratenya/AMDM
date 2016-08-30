using AMDM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.DTO
{
    public class PerformerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string BiographyText { get; set; }
        public virtual ICollection<SongDTO> Songs { get; set; }

        public PerformerDTO()
        {
            Songs = new List<SongDTO>();
        }
    }
}
