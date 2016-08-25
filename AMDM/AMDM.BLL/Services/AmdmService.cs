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
            System.Net.WebClient web = new System.Net.WebClient();
            web.Encoding = UTF8Encoding.UTF8;
            for (int i = 1; i <= 10; i++) {
                string str = web.DownloadString("http://amdm.ru/chords/page" + i + "/");
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);
                var rows = doc.DocumentNode.SelectNodes("//table[@class='items']/tr");
                for (int j = 1; j < rows.Count; j++) {
                    foreach (var cell in rows[i].SelectNodes(".//td"))
                    {
                        Debug.WriteLine(cell.InnerText.Trim());
                    }
                }
                    }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
