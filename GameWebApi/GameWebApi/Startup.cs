using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddMvc ( ).SetCompatibilityVersion ( Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2 );
            //services.AddSingleton<IRepository, FileRepository> ( );
            services.AddSingleton<IRepository, MongoDbRepository> ( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            if ( env.IsDevelopment ( ) )
            {
                app.UseDeveloperExceptionPage ( );
#pragma warning disable CS0618 // Type or member is obsolete
                loggerFactory.AddConsole ( Configuration.GetSection ( "Logging" ) );
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                loggerFactory.AddDebug ( );
#pragma warning restore CS0618 // Type or member is obsolete
            }

            app.UseMvc ( );
        }
    }
}
