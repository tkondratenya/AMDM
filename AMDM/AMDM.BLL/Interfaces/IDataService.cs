using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface IDataService
    {
        void ParsePerformers();
        void ParseSongs();
        void DeleteAllData();
    }
}
