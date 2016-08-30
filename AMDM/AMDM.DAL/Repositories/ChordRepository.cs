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
    public class ChordRepository : BaseRepository<Chord>
    {
        public ChordRepository(AmdmContext context) : base(context)
        {
        }
    }
}
