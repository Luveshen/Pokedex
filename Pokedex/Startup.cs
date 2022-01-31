using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Services;

namespace Pokedex
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
            services.AddHttpClient<IPokemonRetrievalService, PokemonRetrievalService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri($"{Configuration["PokemonApiBaseUrl"]}/{Configuration["PokemonApiVersion"]}/");
            });
            
            services.AddHttpClient<ITranslationService, TranslationService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri($"{Configuration["TranslationServiceUrl"]}/");
            });
            
            services.AddControllers();
            services.AddTransient<IPokemonService, PokemonService>();
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
                endpoints.MapControllerRoute(
                    "Default",
                    "{controller=default}/{action=Index}/{id?}");
            });
        }
    }
}