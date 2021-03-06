﻿using AMDM.BLL.DTO;
using AMDM.BLL.Infrastructure;
using AMDM.BLL.Interfaces;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Services
{
    public class PerformerService : BaseService, IPerformerService
    {

        public PerformerService(IUnitOfWork uow) : base(uow)
        {
        }
        public IEnumerable<PerformerDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Performer>, IEnumerable<PerformerDTO>>(Database.Performers.GetAll());
        }
        public PerformerDTO Get(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for performer", "");
            Performer performer;
            string keyIndex = "performer_" + id;
            if (!Cache.Get(keyIndex, out performer))
            {
                performer = Database.Performers.Get(id.Value);
                Cache.Set(keyIndex, performer,60);
            }
            else
            if (performer == null)
                throw new ValidationException("Can't find performer", "");
            return Mapper.Map<Performer, PerformerDTO>(performer);
        }
    }
}
