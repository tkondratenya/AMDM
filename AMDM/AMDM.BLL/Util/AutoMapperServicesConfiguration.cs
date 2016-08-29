using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMDM.BLL.DTO;
using AMDM.DAL.Entities;
using AutoMapper;

namespace AMDM.BLL.Util
{
    public class AutoMapperServicesConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new PerformerServiceProfile());
                cfg.AddProfile(new SongServiceProfile());
                cfg.AddProfile(new ChordServiceProfile());
            });
            
        }
    }

    public class PerformerServiceProfile : Profile
    {
        public PerformerServiceProfile()
        {
            CreateMap<Performer, PerformerDTO>().MaxDepth(3);
        }
    }
    public class SongServiceProfile : Profile
    {
        public SongServiceProfile()
        {
            CreateMap<Song, SongDTO>().MaxDepth(3);
        }
    }
    public class ChordServiceProfile : Profile
    {
        public ChordServiceProfile()
        {
            CreateMap<Chord, ChordDTO>().MaxDepth(3);
        }
    }
}
