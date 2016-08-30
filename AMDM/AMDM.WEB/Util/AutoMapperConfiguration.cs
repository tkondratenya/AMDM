using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AMDM.BLL.Util;

namespace AMDM.WEB.Util
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EntityToDTOMappingProfile());
                cfg.AddProfile(new DTOToViewModelMappingProfile());
            });

        }
    }
}