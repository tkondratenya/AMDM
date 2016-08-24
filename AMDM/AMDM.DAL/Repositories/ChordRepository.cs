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
    public class ChordRepository : IRepository<Chord>
    {
        private AmdmContext db;

        public ChordRepository(AmdmContext context)
        {
            this.db = context;
        }

        public IEnumerable<Chord> GetAll()
        {
            return db.Chords;
        }
        public Chord Get(int id)
        {
            return db.Chords.Find(id);
        }

        public void Create(Chord book)
        {
            db.Chords.Add(book);
        }

        public void Update(Chord book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Chord> Find(Func<Chord, Boolean> predicate)
        {
            return db.Chords.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Chord book = db.Chords.Find(id);
            if (book != null)
                db.Chords.Remove(book);
        }
    }
}
