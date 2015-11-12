
using Microsoft.Owin;
[assembly: OwinStartup(typeof(CFH.WebAPI.Startup))]

namespace CFH.WebAPI
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
