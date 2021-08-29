
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessRules.Implementation;

using BusinessRules.Interfaces;
using MutantApi;
using System.Linq;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace Mutant
{
    public class Startup
    {
        protected readonly SwaggerConfiguration swaggerConfiguration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            swaggerConfiguration = new SwaggerConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddSwagger(services);

            var conexion = "Server=tcp:meli-server.database.windows.net,1433;Initial Catalog=melidb;Persist Security Info=False;User ID=administrador;Password=admin123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<DataAccess.ContextDb.MainContext>(options =>
                                                                        options
                                                                         .UseSqlServer(conexion)
                                                                         .UseLazyLoadingProxies(false));
            services.AddTransient<IAdn, Adn>();


            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
                    .AddJsonOptions(options => options.JsonSerializerOptions.MaxDepth = int.MaxValue);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerConfiguration.EndpointSwaggerJson, swaggerConfiguration.EndpointDescription);
            });
        }

        protected void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllParametersInCamelCase();

                swagger.SwaggerDoc(swaggerConfiguration.DocNameV1, GetApiInfo);

                swagger.DocInclusionPredicate((docName, description) => true);
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected OpenApiInfo GetApiInfo => new OpenApiInfo
        {
            Title = swaggerConfiguration.DocInfoTitle,
            Version = swaggerConfiguration.DocInfoVersion,
            Description = swaggerConfiguration.DocInfoDescription,
            Contact = GetApiContact
        };
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private OpenApiContact GetApiContact => new OpenApiContact
        {
            Name = swaggerConfiguration.ContactName,
            Url = swaggerConfiguration.ContactUrl,
            Email = swaggerConfiguration.ContactEmail
        };

    }

}
