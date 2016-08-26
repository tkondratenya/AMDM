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
        public Performer GetByName(string name)
        {
            return db.Performers.SingleOrDefault(x => x.Name == name);
        }

        public void Create(Performer performer)
        {
            db.Performers.Add(performer);
        }

        public void Update(Performer performer)
        {
            db.Entry(performer).State = EntityState.Modified;
        }

        public IEnumerable<Performer> Find(Func<Performer, Boolean> predicate)
        {
            return db.Performers.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Performer performer = db.Performers.Find(id);
            if (performer != null)
                db.Performers.Remove(performer);
        }
    }
}
