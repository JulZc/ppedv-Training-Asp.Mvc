using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Videothek.Startup))]
namespace Videothek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
