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
        public Chord GetByName(string name)
        {
            return db.Chords.SingleOrDefault(x => x.Name == name);
        }

        public void Create(Chord chord)
        {
            db.Chords.Add(chord);
        }

        public void Update(Chord chord)
        {
            db.Entry(chord).State = EntityState.Modified;
        }

        public IEnumerable<Chord> Find(Func<Chord, Boolean> predicate)
        {
            return db.Chords.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Chord chord = db.Chords.Find(id);
            if (chord != null)
                db.Chords.Remove(chord);
        }
    }
}
