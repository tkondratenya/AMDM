using AMDM.DAL.EF;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AmdmContext db;
        private PerformerRepository performerRepository;
        private SongRepository songRepository;
        private ChordRepository chordRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new AmdmContext(connectionString);
        }

        public IRepository<Performer> Performers
        {
            get
            {
                if (performerRepository == null)
                    performerRepository = new PerformerRepository(db);
                return performerRepository;
            }
        }

        public ISongRepository Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IChordRepository Chords
        {
            get
            {
                if (chordRepository == null)
                    chordRepository = new ChordRepository(db);
                return chordRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Truncate()
        {
            db.Database.ExecuteSqlCommand("DELETE FROM [Chords]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT([Chords], RESEED, 0)");
            db.Database.ExecuteSqlCommand("DELETE FROM [Songs]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT([Songs], RESEED, 0)");
            db.Database.ExecuteSqlCommand("DELETE FROM [Performers]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT([Performers], RESEED, 0)");
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
