using AMDM.BLL.Interfaces;
using AMDM.BLL.Util;
using AMDM.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDM.BLL.Services
{
    public class BaseService : IBaseService
    {
        public IUnitOfWork Database { get; set; }
        public readonly HttpCache Cache = new HttpCache();
        public BaseService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
