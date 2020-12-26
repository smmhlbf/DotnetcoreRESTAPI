using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using DotnetcoreRESTAPI.Repositories;
using DotnetcoreRESTAPI.Services;
using DotnetcoreRESTAPI.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DotnetcoreRESTAPI
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            var mongodbSetting = Configuration.GetSection("MongoDBSettings").Get<MongoDBSetting>();
            services.AddSingleton<IMongoClient>(ServiceProvider => new MongoClient(mongodbSetting.ConnectionString));
            services.AddSingleton<IRepository,MongoDBRepository>();
            //services.AddSingleton<IRepository,InMemoryRepository>();
            services.AddControllers(option => option.SuppressAsyncSuffixInActionNames = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetcoreRESTAPI", Version = "v1" });
            });
            services.AddHealthChecks().AddMongoDb(
                mongodbSetting.ConnectionString,
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(2),
                tags: new [] { "ready" }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetcoreRESTAPI v1"));
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Check secvices cloud is living
                endpoints.MapHealthChecks("/health/living", new HealthCheckOptions{ Predicate = (_) =>false});
                endpoints.MapHealthChecks("/health/running", new HealthCheckOptions
                { 
                    Predicate = (check) =>check.Tags.Contains("ready"),
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            report.Entries.Select(
                                entity => 
                                new
                                {
                                    name = entity.Key,
                                    status = entity.Value.Status.ToString(),
                                    code = context.Response.StatusCode,
                                    exception = entity.Value.Exception != null ? entity.Value.Exception.Message.ToString() : "none",
                                    duration = entity.Value.Duration.ToString() 
                                }
                            )
                        );
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

            });
        }
    }
}
