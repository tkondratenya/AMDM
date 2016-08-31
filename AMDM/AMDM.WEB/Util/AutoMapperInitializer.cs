using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AMDM.BLL.Util;

namespace AMDM.WEB.Util
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>

                cfg.AddProfiles(new[] {"AMDM.BLL","AMDM.WEB"})
            );
         }
    }
}