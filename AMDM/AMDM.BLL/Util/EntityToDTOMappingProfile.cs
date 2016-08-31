using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AMDM.BLL.DTO;
using AMDM.DAL.Entities;

namespace AMDM.BLL.Util
{
        public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<Performer, PerformerDTO>().MaxDepth(3);
            CreateMap<Song, SongDTO>().MaxDepth(3);
            CreateMap<Chord, ChordDTO>().MaxDepth(3);
            CreateMap<PerformerDTO, Performer>().MaxDepth(3);
            CreateMap<SongDTO, Song>().MaxDepth(3);
            CreateMap<ChordDTO, Chord>().MaxDepth(3);
        }
    }
}
