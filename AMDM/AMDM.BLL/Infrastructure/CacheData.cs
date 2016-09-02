using AMDM.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AMDM.BLL.Infrastructure
{
    public class CacheData
    {
        public T Get<T>(string cacheKey, int durationInMinutes, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(durationInMinutes));
            }
            return item;
        }

        public T Get<T, TId>(string cacheKeyFormat, TId id, int durationInMinutes, Func<TId, T> getItemCallback) where T : class
        {
            string cacheKey = string.Format(cacheKeyFormat, id);
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback(id);
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(durationInMinutes));
            }
            return item;
        }
    }
}
