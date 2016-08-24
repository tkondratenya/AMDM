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
    public class PerformerRepository : IRepository<Performer>
    {
        private AmdmContext db;

        public PerformerRepository(AmdmContext context)
        {
            this.db = context;
        }

        public IEnumerable<Performer> GetAll()
        {
            return db.Performers;
        }
        public Performer Get(int id)
        {
            return db.Performers.Find(id);
        }

        public void Create(Performer book)
        {
            db.Performers.Add(book);
        }

        public void Update(Performer book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Performer> Find(Func<Performer, Boolean> predicate)
        {
            return db.Performers.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Performer book = db.Performers.Find(id);
            if (book != null)
                db.Performers.Remove(book);
        }
    }
}
