using AMDM.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface IAmdmService
    {
        IEnumerable<PerformerDTO> GetPerformers();
        IEnumerable<SongDTO> GetSongs();
        IEnumerable<ChordDTO> GetChords();
        PerformerDTO GetPerformer(int? id);
        SongDTO GetSong(int? id);
        ChordDTO GetChord(int? id);
        void ParseAmdm();
        void Dispose();
    }
}
