using System;
using Core.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitRecognizer {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddCors(co =>
                co.AddPolicy("AccessControlAllowOrigin", cp => cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            var neuralApiUrl = Configuration["NeuralApiUrl"];
            services.AddSingleton(new NeuralClient(neuralApiUrl));
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider) {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}