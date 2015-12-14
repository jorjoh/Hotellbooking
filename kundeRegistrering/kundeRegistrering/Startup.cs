using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kundeRegistrering.Startup))]
namespace kundeRegistrering
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
