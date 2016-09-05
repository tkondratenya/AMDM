using AMDM.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface ISongService
    {
        IEnumerable<SongDTO> GetAll();
        SongDTO Get(int? id);
        int GetSongsCount(int performerId);
        IEnumerable<SongDTO> GetSongsChunkWithOrder(int performerId, string order, int skip, int take);
        void Update(SongDTO song);
    }
}
