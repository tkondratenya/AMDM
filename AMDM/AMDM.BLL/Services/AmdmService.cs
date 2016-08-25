using AMDM.BLL.DTO;
using AMDM.DAL.Repositories;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using AMDM.BLL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMDM.BLL.Infrastructure;
using HtmlAgilityPack;
using System.Diagnostics;

namespace AMDM.BLL.Services
{
    public class AmdmService : IAmdmService
    {
        IUnitOfWork Database { get; set; }

        public AmdmService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<PerformerDTO> GetPerformers()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Performer, PerformerDTO>();
            });
            return Mapper.Map<IEnumerable<Performer>, List<PerformerDTO>>(Database.Performers.GetAll());
        }

        public IEnumerable<SongDTO> GetSongs()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Song, SongDTO>();
            });
            return Mapper.Map<IEnumerable<Song>, List<SongDTO>>(Database.Songs.GetAll());
        }

        public IEnumerable<ChordDTO> GetChords()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Chord, ChordDTO>();
            });
            return Mapper.Map<IEnumerable<Chord>, List<ChordDTO>>(Database.Chords.GetAll());
        }

        public PerformerDTO GetPerformer(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for performer", "");
            var performer = Database.Performers.Get(id.Value);
            if (performer == null)
                throw new ValidationException("Can't find performer", "");
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Performer, PerformerDTO>();
            });
            return Mapper.Map<Performer, PerformerDTO>(performer);
        }

        public SongDTO GetSong(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for song", "");
            var song = Database.Songs.Get(id.Value);
            if (song == null)
                throw new ValidationException("Can't find song", "");
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Song, SongDTO>();
            });
            return Mapper.Map<Song, SongDTO>(song);
        }

        public ChordDTO GetChord(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for chord", "");
            var chord = Database.Chords.Get(id.Value);
            if (chord == null)
                throw new ValidationException("Can't find chord", "");
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Chord, ChordDTO>();
            });
            return Mapper.Map<Chord, ChordDTO>(chord);
        }

        public void ParsePerformers()
        {
            var performerList = new List<Object>();
            var songList = new List<Object>();
            var chordList = new List<Object>();

            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = UTF8Encoding.UTF8;
            for (int i = 1; i <= 10; i++)
            {
                Debug.WriteLine("Parsing page №" + i);
                string str = web.DownloadString("http://amdm.ru/chords/page" + i + "/");
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);
                foreach (var cell in doc.DocumentNode.SelectNodes("//table[@class='items']/tr"))
                {
                    var artistPhotoLink = ("http:" + cell.SelectSingleNode(".//a[@class='photo']").ChildNodes.First().Attributes["src"].Value);
                    var artistLink = ("http:" + cell.SelectSingleNode(".//a[@class='artist']").Attributes["href"].Value);
                    var artistName = cell.SelectSingleNode(".//a[@class='artist']").InnerText;

                    string artistStr = web.DownloadString(artistLink);
                    HtmlDocument artistDoc = new HtmlDocument();
                    artistDoc.LoadHtml(artistStr);

                    HtmlNode songContent = artistDoc.DocumentNode.SelectSingleNode("//div[@class='content-table']");
                    var artistBio = songContent.SelectSingleNode(".//div[@class='artist-profile__bio']").InnerText;
                    HtmlNode songListNode = songContent.SelectSingleNode(".//div[@class='artist-profile-song-list']");

                    var artistSongList = new List<Object>();

                    foreach (var songCell in songListNode.SelectNodes("//table[@id='tablesort']/tr"))
                    {
                        var songName = songCell.SelectSingleNode(".//a[@class='g-link']").InnerText;
                        var songLink = ("http:" + songCell.SelectSingleNode(".//a[@class='g-link']").Attributes["href"].Value);
                        var songViews = Int32.Parse(songCell.SelectSingleNode(".//td[@class='number hidden-phone']").InnerText);
                        
                        string songStr = web.DownloadString(songLink);
                        HtmlDocument songDoc = new HtmlDocument();
                        songDoc.LoadHtml(songStr);

                        HtmlNode chordContent = songDoc.DocumentNode.SelectSingleNode("//div[@class='content-table']");
                        var songText = chordContent.SelectSingleNode("//div[@class='b-podbor__text']/pre").InnerText;
                        Debug.WriteLine(songText + "\n");
                        var songVideoLink = "";
                        var songVideoLinkNode = chordContent.SelectSingleNode("//div[@class='b-video-container']/iframe");
                        if (songVideoLinkNode != null)
                        {
                           songVideoLink = songVideoLinkNode.Attributes["src"].Value;
                        }

                        Debug.WriteLine("song name: " + songName + " views:" + songViews + "\nsong link: " + songLink + "\nvideo link: " + songVideoLink + "\nsong text: " + songText + "\n");

                        var songChordList = new List<Object>();

                        foreach (var chord in chordContent.SelectNodes("//div[@id='song_chords']/img"))
                        {
                            var chordLink = ("http:" + chord.Attributes["src"].Value);
                            var chordName = chord.Attributes["alt"].Value;

                            Debug.WriteLine("chord name: " + chordName + "\nchord link: " + chordLink);

                            songChordList.Add(new { Name = chordName, ImageLink = chordLink});

                            artistSongList.Add(new { Name = songName, Views = songViews, SongPageLink = songLink, VideoLink = songVideoLink, Text = songText, Chords = songChordList });
                        }
                        System.Threading.Thread.Sleep(50);
                    }

                    Debug.WriteLine("artist name: " + artistName + "\nartist link: " + artistLink + "\nphoto link: " + artistPhotoLink + "\nartist bio: " + artistBio);

                    performerList.Add(new { Name = artistName, ImageLink = artistPhotoLink, PerformerPageLink = artistLink, BiographyText = artistBio, Songs = artistSongList });
                }
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
