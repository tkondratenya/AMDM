using AMDM.BLL.Interfaces;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;

namespace AMDM.BLL.Services
{
    class DataService : BaseService, IDataService
    {
        WebClient Web { get; set; }

        public DataService(IUnitOfWork uow) : base(uow)
        {
            Web = new WebClient();
            Web.Encoding = UTF8Encoding.UTF8;
        }
        public void DeleteAllData()
        {
            Database.Truncate();
        }

        public void ParsePerformers()
        {
            var popularPerformersPage = ConfigurationManager.AppSettings.Get("PopularPerformersPage");
            var pages = ConfigurationManager.AppSettings.Get("Pages");
            var performerCell = ConfigurationManager.AppSettings.Get("PerformerCell");
            var performerPhoto = ConfigurationManager.AppSettings.Get("PerformerPhoto");
            var performerLink = ConfigurationManager.AppSettings.Get("PerformerLink");
            var contentTable = ConfigurationManager.AppSettings.Get("ContentTable");
            var performerBio = ConfigurationManager.AppSettings.Get("PerformerBio");
            var performerSongList = ConfigurationManager.AppSettings.Get("PerformerSongList");
            var songCell = ConfigurationManager.AppSettings.Get("SongCell");
            var songLink = ConfigurationManager.AppSettings.Get("SongLink");
            var songViews = ConfigurationManager.AppSettings.Get("SongViews");
            var songText = ConfigurationManager.AppSettings.Get("SongText");
            var songVideo = ConfigurationManager.AppSettings.Get("SongVideo");
            var chordLink = ConfigurationManager.AppSettings.Get("ChordLink"); 
            
                  
        }

        public void ParseSongs()
        {
            
        }

        private string GetDownloadString(string link)
        {
            var str = "";
            while (true)
            {
                try
                {
                    return str = Web.DownloadString(link);
                }
                catch (WebException e)
                {
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
    }
}
