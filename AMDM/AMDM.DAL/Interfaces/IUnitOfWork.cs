using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {     
        IRepository<Performer> Performers { get; }
        ISongRepository Songs { get; }
        IChordRepository Chords { get; }
        void Save();
        void Truncate();
    }
}
