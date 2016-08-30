using AMDM.BLL.Interfaces;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using HtmlAgilityPack;
using System.Diagnostics;
using AMDM.BLL.DTO;
using AMDM.DAL.Entities;

namespace AMDM.BLL.Services
{
    public class DataService : BaseService, IDataService
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

        public List<Performer> ParsePerformers()
        {
            List<Performer> PerformersList = new List<Performer>();

            var popularPerformersPage = ConfigurationManager.AppSettings.Get("PopularPerformersPage");
            var pages = Int32.Parse(ConfigurationManager.AppSettings.Get("Pages"));
            var performerCell = ConfigurationManager.AppSettings.Get("PerformerCell");
            var performerPhoto = ConfigurationManager.AppSettings.Get("PerformerPhoto");
            var performerLink = ConfigurationManager.AppSettings.Get("PerformerLink");
            var contentTable = ConfigurationManager.AppSettings.Get("ContentTable");
            var performerBio = ConfigurationManager.AppSettings.Get("PerformerBio");

            for (int i = 1; i <= pages; i++)
            {
                string str = GetDownloadString(popularPerformersPage + i + "/");
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);
                var performerNodes = doc.DocumentNode.SelectNodes("//table[@class='items']/tr");
                foreach (var cell in performerNodes)
                {
                    // Getting artist photo link
                    var artistPhotoLink = "";
                    var artistPhotoLinkNode = cell.SelectSingleNode(performerPhoto).ChildNodes.First();
                    if (artistPhotoLinkNode != null)
                    {
                        artistPhotoLink = ("http:" + artistPhotoLinkNode.Attributes["src"].Value);
                    }

                    // Getting artist page link
                    var artistLink = "";
                    var artistLinkNode = cell.SelectSingleNode(performerLink);
                    if (artistLinkNode != null)
                    {
                        artistLink = ("http:" + artistLinkNode.Attributes["href"].Value);
                    }

                    // Getting artist name
                    var artistName = "";
                    var artistNameNode = cell.SelectSingleNode(performerLink);
                    if (artistNameNode != null)
                    {
                        artistName = (artistLinkNode.InnerText);
                    }

                    string artistStr = GetDownloadString(artistLink);
                    HtmlDocument artistDoc = new HtmlDocument();
                    artistDoc.LoadHtml(artistStr);
                    HtmlNode songContent = artistDoc.DocumentNode.SelectSingleNode(contentTable);
                    var artistBio = "";
                    var artistBioNode = songContent.SelectSingleNode(performerBio);
                    // Getting artist biography
                    if (artistBioNode != null)
                    {
                        artistBio = artistBioNode.InnerText;
                    }
                    Performer performer = new Performer
                    {
                        Name = artistName,
                        PerformerPageLink = artistLink,
                        ImageLink = artistPhotoLink,
                        BiographyText = artistBio
                    };
                    PerformersList.Add(performer);
                }
            }
            return PerformersList;
        }

        public void StoreParsedPerformers()
        {
            List<Performer> Performers = ParsePerformers();
            foreach(Performer performer in Performers)
            {
                Database.Performers.Create(performer);
            }
            Database.Save();
        }

        public void StoreParsedSongs()
        {
            IEnumerable<Performer> PerformersList = Database.Performers.GetAll();

            foreach (Performer performer in PerformersList)
            {
                List<Song> Songs = ParseSongs(performer);
                foreach (Song song in Songs)
                {
                    IEnumerable<Chord> ChordsList = song.Chords;
                    song.Chords = new List<Chord>();
                    Database.Songs.Create(song);
                    performer.Songs.Add(song);
                    Song songToAdd = Database.Songs.GetByName(song.Name);
                    foreach (Chord chord in ChordsList)
                    {
                        songToAdd.Chords.Add(chord);
                    }
                }
                Database.Save();
            }
        }

        public List<Song> ParseSongs(Performer performer)
        {
            List<Song> SongsList = new List<Song>();
            
            var performerSongList = ConfigurationManager.AppSettings.Get("PerformerSongList");
            var songCell = ConfigurationManager.AppSettings.Get("SongCell");
            var songLink = ConfigurationManager.AppSettings.Get("SongLink");
            var songViews = ConfigurationManager.AppSettings.Get("SongViews");
            var songText = ConfigurationManager.AppSettings.Get("SongText");
            var songVideo = ConfigurationManager.AppSettings.Get("SongVideo");
            var contentTable = ConfigurationManager.AppSettings.Get("ContentTable");

                var artistLink = performer.PerformerPageLink;

                string artistStr = GetDownloadString(artistLink);
                HtmlDocument artistDoc = new HtmlDocument();
                artistDoc.LoadHtml(artistStr);
                HtmlNode songContent = artistDoc.DocumentNode.SelectSingleNode(contentTable);
                HtmlNode songListNode = songContent.SelectSingleNode(performerSongList);
                foreach (var cell in songListNode.SelectNodes(songCell))
                {
                    var songName = "";
                    var songLinkHtml = "";
                    var songNameNode = cell.SelectSingleNode(songLink);
                    // Getting song name
                    if (songNameNode != null)
                    {
                        songName = cell.SelectSingleNode(songLink).InnerText;
                        songLinkHtml = ("http:" + songNameNode.Attributes["href"].Value);
                    }
                    int songViewsHtml = 0;
                    var songViewsNode = cell.SelectSingleNode(songViews);
                    // Getting song views
                    if (songViewsNode != null)
                    {
                        songViewsHtml = Int32.Parse((songViewsNode.InnerText).Replace(",", ""));
                    }
                    string songStr = GetDownloadString(songLinkHtml);
                    HtmlDocument songDoc = new HtmlDocument();
                    songDoc.LoadHtml(songStr);

                    HtmlNode chordContent = songDoc.DocumentNode.SelectSingleNode(contentTable);
                    var songTextHtml = "";
                    var songTextNode = chordContent.SelectSingleNode(songText);
                    // Getting song's text
                    if (songTextNode != null)
                    {
                        songTextHtml = chordContent.SelectSingleNode(songText).InnerText;
                    }
                    var songVideoLink = "";
                    var songVideoLinkNode = chordContent.SelectSingleNode(songVideo);
                    // Getting song's video link
                    if (songVideoLinkNode != null)
                    {
                        songVideoLink = songVideoLinkNode.Attributes["src"].Value;
                    }
                    List<Chord> chords = new List<Chord>();
                    chords = ParseChords(chordContent);
                    Song song = new Song
                    {
                        Name = songName,
                        SongPageLink = songLinkHtml,
                        VideoLink = songVideoLink,
                        Text = songTextHtml,
                        Views = songViewsHtml,
                        Chords = chords
                    };
                    SongsList.Add(song);
                }    
            return SongsList;
        }   

        public List<Chord> ParseChords(HtmlNode chordContent)
        {
            List<Chord> ChordList = new List<Chord>();

            var chordLink = ConfigurationManager.AppSettings.Get("ChordLink");

            var chordNodes = chordContent.SelectNodes(chordLink);
            if (chordNodes != null)
            {
                foreach (var imgNode in chordNodes)
                {
                    var chordLinkHtml = ("http:" + imgNode.Attributes["src"].Value);
                    var chordName = imgNode.Attributes["alt"].Value;
                    Chord chord = new Chord
                    {
                        Name = chordName,
                        ImageLink = chordLinkHtml,
                    };
                    ChordList.Add(chord);
                }
            }
            return ChordList;
        }

        private string GetDownloadString(string link)
        {
            var str = "";
            bool flag = false;
            while (!flag)
            {
                try
                {
                    str = Web.DownloadString(link);
                    flag = true;
                }
                catch (WebException e)
                {
                    flag = false;
                    System.Threading.Thread.Sleep(5000);
                }
            }
            return str;
        }
    }
}
