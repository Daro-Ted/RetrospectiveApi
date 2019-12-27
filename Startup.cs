using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using RetrospectiveApi.MappingProfile;
using RetrospectiveApi.Entities;
using Microsoft.EntityFrameworkCore;
using RetrospectiveApi.Repositories;
using RetrospectiveApi.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;

namespace RetrospectiveApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            //Add cros to enable angular to communicate with the Endpoints
            services.AddCors(option => option.AddPolicy("ApiCorsPolicy", builder => {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Configure Xml return result and return object with thesame case as models
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                if (o.SerializerSettings.ContractResolver == null) return;
                var castedReslover = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                castedReslover.NamingStrategy = null;
            }).AddMvcOptions(x => x.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            //Connection string
            var connectionStrings = Configuration.GetConnectionString("RetrospectiveConnectionString");
            services.AddDbContext<ProjectDBContext>(o => o.UseSqlServer(connectionStrings));
            //register Repository and services
            services.AddScoped<IRetrospectiveRepository, RetrospectiveRepository>(); 
            services.AddScoped<IRetrospectiveService, RetrospectiveService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
     }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProjectDBContext dbcontext,ILoggerFactory loggerFactory)
        {
            //Create Logger to log errors
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("ApiCorsPolicy");

            dbcontext.EnsureDatabaseSeed();

            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<MapProfile>());
            
            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
