﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyStore.Web.Startup))]
namespace MyStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}