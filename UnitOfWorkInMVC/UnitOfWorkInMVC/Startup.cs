using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UnitOfWorkInMVC.Startup))]

namespace UnitOfWorkInMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
