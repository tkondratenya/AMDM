﻿using AMDM.DAL.EF;
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

        public IRepository<Song> Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IRepository<Chord> Chords
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
