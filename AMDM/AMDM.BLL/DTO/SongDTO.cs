using AMDM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SongPageLink { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public string VideoLink { get; set; }
        public virtual ICollection<ChordDTO> Chords { get; set; }

        public int? PerformerId { get; set; }
        public virtual PerformerDTO Performer { get; set; }

        public SongDTO()
        {
            Chords = new List<ChordDTO>();
        }
    }
}
