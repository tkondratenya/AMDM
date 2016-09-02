using AMDM.DAL.EF;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntityWithName
    {
        public AmdmContext db;
        public BaseRepository(AmdmContext context)
        {
            this.db = context;
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
        }

        T IRepository<T>.Get(int id)
        {
            return db.Set<T>().Find(id);
        }

        T IRepository<T>.GetByName(string name)
        {
            return (from e in db.Set<T>()
                    where e.Name == name
                    select e).FirstOrDefault();
        }

        void IRepository<T>.Create(T item)
        {
            db.Set<T>().Add(item);
        }

        void IRepository<T>.Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        void IRepository<T>.Delete(int id)
        {
            T item = db.Set<T>().Find(id);
            if (item != null)
                db.Set<T>().Remove(item);
        }
    }
}
