﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZVSE_Scoreboard.Repositories;

namespace ZVSE_Scoreboard
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

            services.AddMvc ( )
              .SetCompatibilityVersion ( CompatibilityVersion.Version_2_1 );

            services.AddSingleton<IRepository, MongoDb> ( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            if ( env.IsDevelopment ( ) )
            {
                app.UseDeveloperExceptionPage ( );
                loggerFactory.AddConsole ( Configuration.GetSection ( "Logging" ) );
                loggerFactory.AddDebug ( );
            }

            app.UseMvc ( );
        }
    }
}
