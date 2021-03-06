using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadWebApp.Domain;
using FileUploadWebApp.Domain.Interfaces;
using FileUploadWebApp.Repository;
using FileUploadWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileUploadWebApp
{
    public class Startup
    {
        //private readonly ILogger<Startup> logger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //logger = CreateStartupLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { AddXMLComments("ImageUploadAPI", c); });
            services.AddTransient<IDatabaseConnectionFactory>(e => { return new SqlConnectionFactory(Configuration[ServiceConstants.ConnectionString]); });
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImageUploadAPI");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddXMLComments(string apiName, Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
        {
            try
            {
                 c.IncludeXmlComments(string.Format(@"{0}{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, apiName));
            }
            catch (Exception)
            {

            }
        }
    }
}
