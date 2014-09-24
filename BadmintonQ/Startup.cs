using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BadmintonQ.Startup))]
namespace BadmintonQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
