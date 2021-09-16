using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCA.API.LabTests.Extensions;
using HCA.API.LabTests.Model;
using HCA.API.LabTests.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HCA.API.LabTests
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
            services.ConfigureAuth(Configuration);
            services.AddDbContext<LabReportContext>(opt => opt.UseInMemoryDatabase("LabReportDB"));
            services.AddControllers();
            services.AddScoped<ILabTestRepository, LabTestRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ILabReportRepository, LabReportRepository>();
            services.GenerateSawggerDoc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSawggerDoc();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
