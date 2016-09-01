using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMDM.BLL.Interfaces;
using Ninject;
using AMDM.BLL.Services;
using System.Web.Mvc;
using AMDM.BLL.DTO;

namespace AMDM.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IDataService>().To<DataService>();
            kernel.Bind<IPerformerService>().To<PerformerService>();
            kernel.Bind<ISongService>().To<SongService>();
            kernel.Bind<IChordService>().To<ChordService>();
        }
    }
}