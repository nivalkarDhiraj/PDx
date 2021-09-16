using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabReportAPI.Models;
using System.Configuration;

namespace LabReportAPI
{
    public class Startup
    {

        /// <summary>
        /// Application eventview logger instance creation & other variable declaration.
        /// </summary>
        public static ExceptionHandler ExceptionLogger = new ExceptionHandler();
        public static string AbsExpiryAddMin = string.Empty;
        public static string SlidingExpiryAddMin = string.Empty;

        /// <summary>
        /// Class constructor for configuration 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Property to get for configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                AbsExpiryAddMin = Configuration.GetSection("CacheTimer").GetChildren().ElementAt(0).Value;
                SlidingExpiryAddMin = Configuration.GetSection("CacheTimer").GetChildren().ElementAt(1).Value;
                string strConnection = Configuration.GetConnectionString("Patient_DB");
                services.AddDbContext<PatientDbContext>(o => o.UseSqlite(strConnection));
                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LabReportAPI", Version = "v1" });
                });
                services.AddMemoryCache();
            }
            catch (Exception ex)
            {
                ExceptionLogger.WriteEventLogToFile(ex, "Application Start Up - ConfigureServices");
            }

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LabReportAPI v1"));
                }

                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception ex)
            {
                ExceptionLogger.WriteEventLogToFile(ex, "Application Start Up - Configure");
            }
        }
    }
}
