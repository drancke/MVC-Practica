using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Loguate.Startup))]
namespace Loguate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
