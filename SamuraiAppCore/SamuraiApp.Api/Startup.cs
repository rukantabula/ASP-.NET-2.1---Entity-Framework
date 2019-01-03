using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.AspNetCore.Mvc;

namespace SamuraiApp.Api
{
    public class Startup
    {

        public static readonly LoggerFactory ConsoleLoggerFactory
           = new LoggerFactory(new[]
           {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
           });

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SamuraiContext>(
               options => options
               .UseLoggerFactory(ConsoleLoggerFactory)
               .EnableSensitiveDataLogging(true)
               .UseSqlServer(
                   "Server = (localdb)\\mssqllocaldb; Database = SamuraiAppDataCore: Trusted_Connection = True; "));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // Handles bouncing the http request to the https endpoint
            app.UseMvc();
        }
    }
}
