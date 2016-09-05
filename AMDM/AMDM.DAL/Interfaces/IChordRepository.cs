using AMDM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Interfaces
{
    public interface IChordRepository : IRepository<Chord>
    {
        IEnumerable<Chord> GetAllBySongId(int? songId);
    }
}
