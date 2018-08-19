using System;
using DigitRecognizer.Services.Implementation;
using DigitRecognizer.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitRecognizer {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);                

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddCors(co =>
                co.AddPolicy("AccessControlAllowOrigin", cp => cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddSingleton<INeuralService, NeuralService>();
        }
        
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider) {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}