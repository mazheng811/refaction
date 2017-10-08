using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefactorMeNetCore.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using Newtonsoft.Json.Serialization;
using RefactorMeNetCore.ExceptionHandler;

namespace RefactorMeNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IProductService, ProductService>();
            services.TryAddSingleton<IOptionService, OptionService>();
            services.TryAddSingleton<IProductRepository, ProductRepository>();
            services.TryAddSingleton<IOptionRepository, OptionRepository>();
            
            // Add framework services.
            //add json options to make it case sensitive
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());            

            var connection = @"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\refaction\RefactorMeNetCore\Data\Database.mdf;Integrated Security=True;";
            
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseMvc();
        }
    }
}
