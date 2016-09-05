using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AMDM.BLL.DTO;
using AMDM.WEB.Models;

namespace AMDM.WEB.Util
{
    public class DTOToViewModelMappingProfile : Profile
    {
        public DTOToViewModelMappingProfile()
        {
            CreateMap<PerformerDTO, PerformerViewModel>()
                .ForMember(x => x.Songs, opt => opt.Ignore());
            CreateMap<SongDTO, SongViewModel>()
                .ForMember(x => x.Performer, opt => opt.Ignore())
                .ForMember(x => x.Chords, opt => opt.Ignore());
            CreateMap<ChordDTO, ChordViewModel>()
                .ForMember(x => x.Songs, opt => opt.Ignore());
        }
    }
}