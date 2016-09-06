using AMDM.BLL.DTO;
using AMDM.BLL.Infrastructure;
using AMDM.BLL.Interfaces;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Services
{
    public class SongService : BaseService, ISongService
    {
        public SongService(IUnitOfWork uow) : base(uow)
        {
        }
        public IEnumerable<SongDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Song>, List<SongDTO>>(Database.Songs.GetAll());
        }
        public SongDTO Get(int id)
        {
           /* if (id == null)
                throw new ValidationException("There are no such id for song", "");*/
            var song = Database.Songs.Get(id);
            if (song == null)
                throw new ValidationException("Can't find song", "");
            return Mapper.Map<Song, SongDTO>(song);
        }
        public int GetSongsCount(int performerId)
        {
            return Database.Songs.GetSongsCount(performerId);
        }

        public IEnumerable<SongDTO> GetSongsChunkWithOrder(int performerId, string order, int skip, int take)
        {
            IEnumerable<Song> songs = Database.Songs.GetSongsChunkWithOrder(performerId, order, skip, take);
            return Mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(
                Database.Songs.GetSongsChunkWithOrder(performerId, order, skip, take));
        }

        public void Update(SongDTO songDto, int[] chordsId)
        {
            Song song = Mapper.Map<SongDTO, Song>(songDto);
            Song songToChange = Database.Songs.Get(song.Id);
            songToChange.Name = song.Name;
            songToChange.SongPageLink = song.SongPageLink;
            songToChange.Text = song.Text;
            songToChange.VideoLink = song.VideoLink;
            songToChange.Views = song.Views;
            if (chordsId == null)
            {
                song.Chords = new List<Chord>();
            }
            else
            {
                Database.Songs.UpdateChords(songToChange, chordsId);
            }
            Database.Songs.Update(songToChange);
            Database.Save();
        }
    }
}
