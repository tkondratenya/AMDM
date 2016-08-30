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
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<Performer, PerformerDTO>().MaxDepth(3);
            CreateMap<Song, SongDTO>().ReverseMap();
            CreateMap<Chord, ChordDTO>().ReverseMap();
        }
    }
}
