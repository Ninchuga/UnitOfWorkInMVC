using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UniotOfWorkInMVC.Startup))]

namespace UniotOfWorkInMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
