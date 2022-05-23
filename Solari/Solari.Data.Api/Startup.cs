using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Solari.Data.Access;
using Solari.Data.Access.Contracts.Repositories;
using Solari.Data.Access.Repositories;
using System;
using System.IO;
using System.Text.Json.Serialization;

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

            _ = services.AddControllers()
                // Use JSON References to prevent cycles in data.
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
                // Turn of automatic 400 model validation.
                .ConfigureApiBehaviorOptions(options =>
                    options.SuppressModelStateInvalidFilter = true);


            // API name and version.
            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Solari.Data.Api", Version = "v1" });

                string dataApiDocFilePath = Path.Combine(AppContext.BaseDirectory, "Solari.Data.Api.xml");
                string dataAccessDocFilePath = Path.Combine(AppContext.BaseDirectory, "Solari.Data.Access.xml");

                // Adding XML documentation to the Swagger UI
                c.IncludeXmlComments(dataApiDocFilePath);
                c.IncludeXmlComments(dataAccessDocFilePath);
            });

            // Automatic repository and context dependency injection.
            _ = services.AddScoped<SolariContext, SolariContext>();
            _ = services.AddScoped<IAirlineRepository, SqlAirlineRepository>();
            _ = services.AddScoped<IAirportRepository, SqlAirportRepository>();
            _ = services.AddScoped<IFlightRepository, SqlFlightRepository>();
        }

        // This method gets called by the runtime.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solari.Data.Api v1"));
            }

            _ = app.UseHttpsRedirection();

            _ = app.UseRouting();

            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
