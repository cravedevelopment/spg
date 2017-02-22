using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SPG.Data.CQRS;
using SPG.Data.CQRS.CommandHandlers;
using SPG.Data.CQRS.Commands;
using SPG.Data.CQRS.WriteModel;
using SPG.Data.EF;
using System.IO;

namespace SPG.WebAPI
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

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();

            //var host = new WebHostBuilder() 
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(Data.EF.IRepository<>), typeof(Data.EF.Repository<>));

            //services.Configure<IISOptions>(options =>
            //{

            //});


            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            //});
            //services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //// Shows UseCors with CorsPolicyBuilder.
            //app.UseCors(builder =>
            //builder.WithOrigins("http://example.com").AllowAnyMethod().AllowAnyHeader());



            var dataText = System.IO.File.ReadAllText(@"Tools/seeddata.json");
            Seeder.Seedit(dataText, app.ApplicationServices);


            var connection = EventStoreConfiguration.CreateConnection(Configuration.GetSection("EventStoreConfig:EventStorePort").Value,
           Configuration.GetConnectionString("EventStoreConfig:EventStoreHostName"));
            var commandBus = new CommandBus();
            var repository = new EventStoreRepository<SampleModel>(connection);
            var commands = new SampleCommandHandler(repository);
            commandBus.RegisterHandler<CreateSampleCommand>(commands.Handle);
            ServiceLocator.Bus = commandBus;
        }
    }
}
