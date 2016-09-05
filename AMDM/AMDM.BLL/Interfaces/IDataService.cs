using AMDM.DAL.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface IDataService
    {
        List<Performer> ParsePerformers();
        List<Song> ParseSongs(Performer performer);
        void StoreParsedPerformers();
        void StoreParsedSongs();
        void DeleteAllData();
        void ClearCache();
    }
}
