using AMDM.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Interfaces
{
    public interface IPerformerService
    {
        IEnumerable<PerformerDTO> GetAll();
        PerformerDTO Get(int? id);
    }
}
