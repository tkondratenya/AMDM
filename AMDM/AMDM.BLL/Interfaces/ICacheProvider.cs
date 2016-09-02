using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface ICacheProvider
    {
        bool Get<T>(string key, out T value);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, int duration);     
        void Clear(string key);
        IEnumerable<KeyValuePair<string, object>> GetAll();
    }
}
