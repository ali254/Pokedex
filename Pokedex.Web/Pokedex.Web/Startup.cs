using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pokedex.Core.AutoMapper;
using Pokedex.Data;
using Pokedex.Service;
using Pokedex.Web.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Web
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

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddHttpClient<IPokeApiConnection, PokeApiConnection>(c => 
                c.BaseAddress = new Uri(Configuration.GetSection("pokeapi_baseurl").Value));
            services.AddHttpClient<IFunTranslationConnection, FunTranslationConnection>(c =>
                c.BaseAddress = new Uri(Configuration.GetSection("funtranslation_baseurl").Value));

            services.AddTransient<IPokemonService, PokemonService>();


            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Pokedex Api", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x => 
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex Api");
                x.DefaultModelsExpandDepth(-1); //remove schemas
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
