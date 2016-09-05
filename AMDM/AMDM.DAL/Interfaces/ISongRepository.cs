using AMDM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Interfaces
{
    public interface ISongRepository : IRepository<Song>
    {
        int GetSongsCount(int performerId);
        IEnumerable<Song> GetSongsChunkWithOrder(int performerId, string order, int skip, int take);
    }
}
