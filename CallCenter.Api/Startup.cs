using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CallCenter.Data;
using CallCenter.Data.Repository;
using CallCenter.Data.Seed;
using AutoMapper;
using CallCenter.Api.ViewModels.Mappings;
using Swashbuckle.Swagger.Model;
using CallCenter.Data.Settings;

namespace CallCenter.Api
{
    public class Startup
    {

        private MapperConfiguration MapperConfig { get; set; }

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            MapperConfig = AutoMapperConfiguration.Configure();
           


        }

        

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddOptions();
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            
            services.AddSingleton<IMapper>(sp => MapperConfig.CreateMapper());

            services.AddDbContext<CallCenterContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<ICallRepository, CallRepository>();

            services.AddTransient<CallCenterContextSeedData>();

            services.AddMvc();

            


            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Call Center Api",
                    Description = "Call Center Api for the application",
                    TermsOfService = "None"
                });
                //options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            CallCenterContextSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsureSeedData().Wait();
            
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
