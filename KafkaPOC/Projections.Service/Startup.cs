using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projections.Common;
using Swashbuckle.AspNetCore.Swagger;

namespace Projections.Service
{
    public class Startup
    {
        private string _environmentName = "development";
        private string _corsPolicy = "ACVPolicy";

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            _environmentName = env.EnvironmentName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings." + env.EnvironmentName + ".json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(_corsPolicy, builder =>
            {
                //TODO: for now this works....  Might want to restrict this or off-load the cors to NGINX or something...
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            if (_environmentName.ToLower() == "development")
            {
                // Register the Swagger generator, defining one or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "ACV Identity API", Version = "v1" });
                });
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            IoCSetup.CustomSetup(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (_environmentName.ToLower() == "development")
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACV Identity API V1");
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(_corsPolicy);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
