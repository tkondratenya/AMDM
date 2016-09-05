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
            CreateMap<Performer, PerformerDTO>()
                .ForMember(x => x.Songs, opt => opt.Ignore());
            CreateMap<Song, SongDTO>()
                .ForMember(x => x.Performer, opt => opt.Ignore())
                .ForMember(x => x.Chords, opt => opt.Ignore());
            CreateMap<Chord, ChordDTO>()
                .ForMember(x => x.Songs, opt => opt.Ignore());
            CreateMap<SongDTO, Song>()
                .ForMember(x => x.Performer, opt => opt.Ignore());
            CreateMap<ChordDTO, Chord>();
        }
    }
}
