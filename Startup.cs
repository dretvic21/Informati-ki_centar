using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IcKatalog.Startup))]
namespace IcKatalog
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
