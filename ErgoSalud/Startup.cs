using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ErgoSalud.Startup))]
namespace ErgoSalud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
