using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieProphecy.Startup))]
namespace MovieProphecy
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
