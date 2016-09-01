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
        public SongDTO Get(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for song", "");
            var song = Database.Songs.Get(id.Value);
            if (song == null)
                throw new ValidationException("Can't find song", "");
            return Mapper.Map<Song, SongDTO>(song);
        }
        public IEnumerable<SongDTO> GetSongsByPerformerId(int? performerId)
        {
            return Mapper.Map<IEnumerable<Song>, List<SongDTO>>(Database.GetSongsByPerformerId(performerId));
        }
    }
}
