using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TechBuddy.Middlewares.ExceptionHandling;

namespace TechBuddy.ExceptionHandlingTest
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechBuddy.ExceptionHandlingTest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechBuddy.ExceptionHandlingTest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.AddTBExceptionHandlingMiddleware(opt =>
            {
                //opt.IsDevelopment = env.IsDevelopment();
                opt.DefaultHttpStatusCode = HttpStatusCode.BadGateway;
                opt.ContentType = "application/json";
                opt.DefaultMessage = "My custom default message";

                //opt.ExceptionHandlerAction = async (httpContext, ex) => 
                //{
                //    httpContext.Response.StatusCode = 
                //            (int)HttpStatusCode.Forbidden;

                //    await httpContext.Response.WriteAsync("Custom Http Context");

                //    Console.WriteLine(ex.ToString());
                //};

                opt.ExceptionTypeList.Add<Exception>();
                opt.ExceptionTypeList.Add<ArgumentNullException>();

                //opt.UseResponseModelCreator<CustomResponseModelCreator>();

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
