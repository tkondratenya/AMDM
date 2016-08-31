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
            CreateMap<PerformerDTO, PerformerViewModel>().MaxDepth(3);
            CreateMap<SongDTO, SongViewModel>().MaxDepth(3);
            CreateMap<ChordDTO, ChordViewModel>().MaxDepth(3);
            CreateMap<PerformerViewModel, PerformerDTO>().MaxDepth(3);
            CreateMap<SongViewModel, SongDTO>().MaxDepth(3);
            CreateMap<ChordViewModel, ChordDTO>().MaxDepth(3);
        }
    }
}