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
    public class SongRepository : IRepository<Song>
    {
        private AmdmContext db;

        public SongRepository(AmdmContext context)
        {
            this.db = context;
        }

        public IEnumerable<Song> GetAll()
        {
            return db.Songs;
        }
        public Song Get(int id)
        {
            return db.Songs.Find(id);
        }

        public void Create(Song song)
        {
            db.Songs.Add(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public IEnumerable<Song> Find(Func<Song, Boolean> predicate)
        {
            return db.Songs.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Song song = db.Songs.Find(id);
            if (song != null)
                db.Songs.Remove(song);
        }
    }
}
