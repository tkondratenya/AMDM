using AMDM.BLL.DTO;
using AMDM.BLL.Infrastructure;
using AMDM.BLL.Interfaces;
using AMDM.DAL.Entities;
using AMDM.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Services
{
    public class ChordService : BaseService, IChordService
    {

        public ChordService(IUnitOfWork uow) : base(uow)
        {
        }
        public IEnumerable<ChordDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Chord>, List<ChordDTO>>(Database.Chords.GetAll());
        }
        public ChordDTO Get(int? id)
        {
            if (id == null)
                throw new ValidationException("There are no such id for chord", "");
            var chord = Database.Chords.Get(id.Value);
            if (chord == null)
                throw new ValidationException("Can't find chord", "");
            return Mapper.Map<Chord, ChordDTO>(chord);
        }
    }
}
