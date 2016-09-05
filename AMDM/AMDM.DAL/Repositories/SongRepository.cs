﻿using AMDM.DAL.EF;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.DAL.Repositories
{
    public class SongRepository : BaseRepository<Song>
    {
        public SongRepository(AmdmContext context) : base(context)
        {
        }

        public int GetSongsCount(int performerId)
        {
            return db.Songs.Where(s => s.PerformerId == performerId).Count();
        }

        public IEnumerable<Song> GetSongsChunkWithOrder(int performerId, string order, int skip, int take)
        {
            IEnumerable<Song> songs = new List<Song>();
            if (order.Equals("views_desc"))
            {
                songs = db.Songs.Where(s => s.PerformerId == performerId)
                .OrderBy(s => s.Views)
                .Skip((skip) * take)
                .Take(take);
            }
            else
            {
                songs = db.Songs.Where(s => s.PerformerId == performerId)
                    .OrderBy(s => s.Views)
                    .Skip((skip - 1) * take)
                    .Take(take);
            }
            return songs;
        }
    }
}
