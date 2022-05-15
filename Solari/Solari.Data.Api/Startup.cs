using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Solari.Data.Access;
using Solari.Data.Access.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Solari.Data.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {

            // Use JSON References to prevent cycles in data.
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            // API name and version.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Solari.Data.Api", Version = "v1" });

                var dataApiDocFilePath = Path.Combine(AppContext.BaseDirectory, "Solari.Data.Api.xml");
                var dataAccessDocFilePath = Path.Combine(AppContext.BaseDirectory, "Solari.Data.Access.xml");

                // Adding XML documentation to the Swagger UI
                c.IncludeXmlComments(dataApiDocFilePath);
                c.IncludeXmlComments(dataAccessDocFilePath);
            });

            // Automatic repository and context dependency injection.
            services.AddScoped<SolariContext, SolariContext>();
            services.AddScoped<IAirlineRepository, SqlAirlineRepository>();
            services.AddScoped<IAirportRepository, SqlAirportRepository>();
            services.AddScoped<IFlightRepository, SqlFlightRepository>();
        }

        // This method gets called by the runtime.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solari.Data.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
