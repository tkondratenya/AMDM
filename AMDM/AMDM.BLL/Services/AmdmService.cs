﻿using AMDM.BLL.DTO;
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
using System.Net;
using System.Collections;

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

        public void ParseAmdm()
        {
            bool artistWebPass = false;
            bool songWebPass = false;
            bool pageWebPass = false;
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.UTF8;
            for (int i = 1; i <= 10; i++)
            {
                Debug.WriteLine("Parsing page №" + i);             
                string str = "";
                pageWebPass = false;
                // Trying to access appropriate amdm.ru/chords/page
                while (!pageWebPass)
                {
                    try
                    {
                        str = web.DownloadString("http://amdm.ru/chords/page" + i + "/");
                        pageWebPass = true;
                    }
                    catch (WebException e)
                    {
                        pageWebPass = false;
                        Debug.WriteLine("WEB EXCEPTION! Waiting 5 seconds!");
                        System.Threading.Thread.Sleep(5000);
                    }
                }
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);
                foreach (var cell in doc.DocumentNode.SelectNodes("//table[@class='items']/tr"))
                {
                    var artistPhotoLink = "";
                    var artistPhotoLinkNode = cell.SelectSingleNode(".//a[@class='photo']").ChildNodes.First();
                    // Getting artist photo link
                    if (artistPhotoLinkNode != null)
                    {
                        artistPhotoLink = ("http:" + artistPhotoLinkNode.Attributes["src"].Value);
                    }
                    var artistLink = "";
                    var artistLinkNode = cell.SelectSingleNode(".//a[@class='artist']");
                    // Getting artist page link
                    if (artistLinkNode != null)
                    {
                        artistLink = ("http:" + artistLinkNode.Attributes["href"].Value);
                    }
                    var artistName = "";
                    var artistNameNode = cell.SelectSingleNode(".//a[@class='artist']");
                    // Getting artist name
                    if (artistNameNode != null)
                    {
                        artistName = (artistLinkNode.InnerText);
                    }
                    string artistStr = "";
                    artistWebPass = false;
                    // Trying to access artist's page
                    while (!artistWebPass)
                    {
                        try
                        {
                            artistStr = web.DownloadString(artistLink);
                            artistWebPass = true;
                        }
                        catch (WebException e)
                        {
                            artistWebPass = false;
                            Debug.WriteLine("WEB EXCEPTION! Waiting 5 seconds!");
                            System.Threading.Thread.Sleep(5000);
                        }
                    }
                    HtmlDocument artistDoc = new HtmlDocument();
                    artistDoc.LoadHtml(artistStr);
                    HtmlNode songContent = artistDoc.DocumentNode.SelectSingleNode("//div[@class='content-table']");
                    var artistBio = "";
                    var artistBioNode = songContent.SelectSingleNode(".//div[@class='artist-profile__bio']");
                    // Getting artist biography
                    if(artistBioNode != null)
                    {
                        artistBio = artistBioNode.InnerText;
                    }
                    // Debug message with artist's data
                    Debug.WriteLine("artist name: " + artistName + "\nartist link: " + artistLink + "\nphoto link: " + artistPhotoLink + "\nartist bio: " + artistBio); 
                    HtmlNode songListNode = songContent.SelectSingleNode(".//div[@class='artist-profile-song-list']");
                    var count = 0;
                    // List to store songs for performer object
                    List<Song> songList = new List<Song>();
                    foreach (var songCell in songListNode.SelectNodes("//table[@id='tablesort']/tr"))
                    {
                        var songName = "";
                        var songNameNode = songCell.SelectSingleNode(".//a[@class='g-link']");
                        // Getting song name
                        if(songNameNode != null)
                        {
                            songName = songCell.SelectSingleNode(".//a[@class='g-link']").InnerText;
                        }  
                        var songLink = "";
                        var songLinkNode = songCell.SelectSingleNode(".//a[@class='g-link']");
                        // Getting song link
                        if (songLinkNode != null)
                        {
                            songLink = ("http:" + songLinkNode.Attributes["href"].Value);
                        }
                        var songViews = "";
                        var songViewsNode = songCell.SelectSingleNode(".//td[@class='number hidden-phone']");
                        // Getting song views
                        if (songViewsNode != null) {
                            songViews = songViewsNode.InnerText;
                        }

                        string songStr = "";
                        songWebPass = false;
                        // Trying to access song's page
                        while (!songWebPass)
                        {
                            try
                            {
                                songStr = web.DownloadString(songLink);
                                songWebPass = true;
                            }
                            catch (WebException e)
                            {
                                songWebPass = false;
                                Debug.WriteLine("WEB EXCEPTION! Waiting 5 seconds!");
                                System.Threading.Thread.Sleep(5000);
                            }
                        }
                        count++;
                        // Debug message with songs count
                        Debug.WriteLine("Parsing Song№" + count);
                        HtmlDocument songDoc = new HtmlDocument();
                        songDoc.LoadHtml(songStr);
                        HtmlNode chordContent = songDoc.DocumentNode.SelectSingleNode("//div[@class='content-table']");
                        var songText = "";
                        var songTextNode = chordContent.SelectSingleNode("//div[@class='b-podbor__text']/pre");
                        // Getting song's text
                        if (songTextNode != null)
                        {
                            songText = chordContent.SelectSingleNode("//div[@class='b-podbor__text']/pre").InnerText;
                        }
                        var songVideoLink = "";
                        var songVideoLinkNode = chordContent.SelectSingleNode("//div[@class='b-video-container']/iframe");
                        // Getting song's video link
                        if (songVideoLinkNode != null)
                        {
                            songVideoLink = songVideoLinkNode.Attributes["src"].Value;
                        }
                        // Debug message with song's data
                        Debug.WriteLine("song name: " + songName + " views:" + songViews + "\nsong link: " + songLink + "\nvideo link: " + songVideoLink + "\nsong text: " + songText + "\n");
                        var chordNodes = chordContent.SelectNodes("//div[@id='song_chords']/img");
                        ICollection<Chord> chordList = new List<Chord>();
                        // Getting chords data
                        if (chordNodes != null)
                        {
                            foreach (var imgNode in chordNodes)
                            {
                                var chordLink = ("http:" + imgNode.Attributes["src"].Value);
                                var chordName = imgNode.Attributes["alt"].Value;
                                //Debug message with chord data
                                Debug.WriteLine("chord name: " + chordName + "\nchord link: " + chordLink);
                                // Creating chord object to save in database
                                Chord chord = new Chord
                                {
                                    Name = chordName,
                                    ImageLink = chordLink,
                                };
                                Database.Chords.Create(chord);
                                chordList.Add(chord);
                                Database.Save();
                            }
                        }
                        int intViews = 0;
                        songViews = songViews.Replace(",", "");
                        Int32.TryParse(songViews, out intViews);
                        // Creating song object to save in database     
                        Song song = new Song
                        {
                            Name = songName,
                            SongPageLink = songLink,
                            Text = songText,
                            Chords = chordList,
                            VideoLink = songVideoLink,
                            Views = intViews
                        };
                        Database.Songs.Create(song);
                        songList.Add(song);
                        Database.Save();
                    }
                    // Creating performer object to save in database 
                    Performer performer = new Performer
                    {
                        Name = artistName,
                        ImageLink = artistPhotoLink,
                        Songs = songList,
                        PerformerPageLink = artistLink,
                        BiographyText = artistBio
                    };
                    Database.Performers.Create(performer);
                    Database.Save();
                    Debug.WriteLine("Saved changes in database!");
                }
            }
            Debug.WriteLine("ENDED SUCCESSFULLY");
            Debug.WriteLine("ENDED SUCCESSFULLY");
            Debug.WriteLine("ENDED SUCCESSFULLY");
        }

        public void DeleteAllData()
        {
           foreach( Performer performer in Database.Performers.GetAll())
            {
                Database.Performers.Delete(performer.Id);
            }
           foreach(Song song in Database.Songs.GetAll())
            {
                Database.Songs.Delete(song.Id);
            }
            foreach (Chord chord in Database.Chords.GetAll())
            {
                Database.Chords.Delete(chord.Id);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
