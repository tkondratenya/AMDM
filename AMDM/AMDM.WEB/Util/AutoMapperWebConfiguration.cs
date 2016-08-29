using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMDM.BLL.DTO;
using AMDM.WEB.Models;
using AutoMapper;

namespace AMDM.WEB.Util
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new PerformerWebProfile());
                cfg.AddProfile(new SongWebProfile());
                cfg.AddProfile(new ChordWebProfile());
            });
            Mapper.AssertConfigurationIsValid();
        }
    }

    public class PerformerWebProfile : Profile
    {
        public PerformerWebProfile()
        {
            CreateMap<PerformerDTO, PerformerViewModel>().MaxDepth(3);
        }
    }
    public class SongWebProfile : Profile
    {
        public SongWebProfile()
        {
            CreateMap<SongDTO, SongViewModel>().MaxDepth(3);
        }
    }
    public class ChordWebProfile : Profile
    {
        public ChordWebProfile()
        {
            CreateMap<ChordDTO, ChordViewModel>().MaxDepth(3);
        }
    }
}