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
            CreateMap<PerformerDTO, PerformerViewModel>().ReverseMap().MaxDepth(3);
            CreateMap<SongDTO, SongViewModel>().ReverseMap().MaxDepth(3);
            CreateMap<ChordDTO, ChordViewModel>().ReverseMap().MaxDepth(3);
        }
    }
}