using AppSolutions.TeamServer.Controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppSolutions.TeamServer
{
    public class Startup
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IWebHostEnvironment env)
        //{
        //    _logger.Info("Startup:ctor");
        //    // In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();

        //    this.Configuration = builder.Build();   
        //}

        public IConfiguration Configuration { get; }

        //public ILifetimeScope AutofacContainer { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Info("Startup:ConfigureServices");
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called. Don't create a ContainerBuilder
            // for Autofac here, and don't call builder.Populate() - that
            // happens in the AutofacServiceProviderFactory for you.
            //services.AddAutofac();
            services.AddOptions();
            services.AddControllersWithViews();

            // Add controllers as services so they'll be resolved.
            services.AddMvc().AddControllersAsServices();

            services.TryAddTransient<Platform.Services.UserManagement.IUserManagementService, Platform.Services.UserManagement.Impl.UserManagementServiceImpl>();
            services.TryAddTransient<Platform.Services.MailManagement.IMailManagementService, Platform.Services.MailManagement.Impl.MailManagementServiceImpl>();

            services.TryAddTransient<Platform.Services.DataRepository.IMailManagementDataRepositoryService, Platform.Services.DataRepository.Impl.MailManagementFileDataRepository>();
            services.TryAddSingleton<Platform.Services.DataRepository.IUserManagementDataRepositoryService, Platform.Services.DataRepository.Impl.UserManagementFileDataRepository>();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    _logger.Info("Startup:ConfigureContainer");
        //    // Register your own things directly with Autofac here. Don't
        //    // call builder.Populate(), that happens in AutofacServiceProviderFactory
        //    // for you.
        //    builder.RegisterAssemblyModules(Assemblies);

        //    // If you want to set up a controller for, say, property injection
        //    // you can override the controller registration after populating services.
        //    builder.RegisterType<RegisteredClientsController>().PropertiesAutowired();
        //}

        //private Assembly[] Assemblies
        //{
        //    get
        //    {
        //        var assemblies = new List<Assembly>();

        //        string modulePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //        _logger.Info("load modules from " + modulePath);
        //        if (Directory.Exists(modulePath))
        //        {
        //            foreach (string dll in Directory.GetFiles(modulePath, "*.*")
        //                .Where(f => f.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)))
        //            {
        //                try
        //                {
        //                    _logger.Debug($"load DLL for autofac module scanning:{dll}");
        //                    assemblies.Add(Assembly.LoadFile(dll));
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.Error(ex);
        //                }
        //            }
        //        }

        //        return assemblies.ToArray();
        //    }
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _logger.Info("Startup:Configure");
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            //var config = GlobalConfiguration.Configuration;
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(this.AutofacContainer);
            //this.AutofacContainer.ComponentRegistry.Registrations.Where(o => o.)
            //var x = this.AutofacContainer.Resolve<UserManagement.Services.IUserManagementService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
