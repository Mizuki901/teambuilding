using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamBuilding.Startup))]
namespace TeamBuilding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
