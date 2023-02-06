using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Work;
using System.Reflection;

namespace RecordPoint.Connectors.SDK.WebHost
{
    /// <summary>
    /// Web server startup
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var corsOrigins = _configuration.GetSection("CorsOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    configurePolicy =>
                    {
                        configurePolicy
                            .WithOrigins(corsOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Setup Authentication
            var authenticationConfiguration = _configuration.GetSection(WebHostAuthenticationOptions.SECTION_NAME)
                .Get<WebHostAuthenticationOptions>();
            if (authenticationConfiguration != null)
            {
                services.AddMicrosoftIdentityWebApiAuthentication(_configuration, WebHostAuthenticationOptions.SECTION_NAME);
            }

            // Add controllers
            services
                .AddControllers(o =>
                {
                    if (authenticationConfiguration != null)
                    {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        o.Filters.Add(new AuthorizeFilter(policy));
                    }
                })
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(Startup).Assembly));
            
            // Add Swagger
            services.AddSwaggerGen(config=>
            {                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    config.IncludeXmlComments(xmlPath);
                }
                
            });            
            
            services.AddWorkStateManagement<DatabaseManagedWorkStatusManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISystemContext systemContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            var pathBase = $"/{systemContext.GetShortName()}";
            app.UsePathBase(pathBase);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecordPoint Connectors SDK WebHost API");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Configure the root end-point to return a 200 response
                endpoints.MapGet("/", context =>
                {
                    context.Response.StatusCode = 200;
                    return Task.CompletedTask;
                });
            });

        }
    }
}
