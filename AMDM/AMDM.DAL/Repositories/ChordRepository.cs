using AMDM.DAL.EF;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Repositories
{
    public class ChordRepository : BaseRepository<Chord>, IChordRepository
    {
        public ChordRepository(AmdmContext context) : base(context)
        {
        }

        public IEnumerable<Chord> GetAllBySongId(int? songId)
        {
            return db.Chords.Where(x => x.Songs.Any(y=>y.Id == songId));
        }
    }
}
