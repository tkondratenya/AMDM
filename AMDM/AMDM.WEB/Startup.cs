using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AMDM.WEB.Startup))]
namespace AMDM.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
