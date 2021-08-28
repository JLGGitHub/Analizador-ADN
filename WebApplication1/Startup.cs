
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessRules.Implementation;

using BusinessRules.Interfaces;

namespace Mutant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            var conexion = "Server=tcp:meli-server.database.windows.net,1433;Initial Catalog=melidb;Persist Security Info=False;User ID=administrador;Password=admin123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<DataAccess.ContextDb.MainContext>(options =>
                                                                        options
                                                                         .UseSqlServer(conexion)
                                                                         .UseLazyLoadingProxies(false));
            //services.AddScoped<DataAccess.ContextDb.MainContext>(provider => provider.GetService<DataAccess.ContextDb.MainContext>());
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
        }
    }
}
