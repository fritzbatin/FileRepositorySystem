using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smits.Etg.FileRepositorySystem.Web.Startup))]
namespace Smits.Etg.FileRepositorySystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
