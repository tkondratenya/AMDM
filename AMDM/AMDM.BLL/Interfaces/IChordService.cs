using AMDM.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface IChordService
    {
        IEnumerable<ChordDTO> GetAll();
        ChordDTO Get(int? id);

        IEnumerable<ChordDTO> GetAllBySongId(int? songId);
    }
}
