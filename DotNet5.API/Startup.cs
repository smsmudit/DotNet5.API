using DotNet5.API.Configurations;
using DotNet5.API.Data;
using DotNet5.API.IRepository;
using DotNet5.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5.API
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
            services.AddDbContext<DatabaseContext>(x =>
                         x.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
            );
            services.AddAuthentication();
            services.ConfigureIdentity();

            services.AddCors(a =>
            {
                a.AddPolicy("AllowAll", builder =>
                         builder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                );
            });
            services.AddAutoMapper(typeof(MapperInitilizer));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new OpenApiInfo { Title = "Dotnet5.API", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet5.API v1"));
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
