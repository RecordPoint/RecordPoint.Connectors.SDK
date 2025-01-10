using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Work;
using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;

namespace RecordPoint.Connectors.SDK.WebHost
{
    /// <summary>
    /// Web server startup
    /// </summary>
    public class Startup(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Register services into the IServiceCollection.
        /// </summary>
        /// <param name="services">The service collection to register the services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var corsOrigins = _configuration.GetSection("CorsOrigins").Get<string[]>();
            if (corsOrigins != null)
            {
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
            }

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Setup Multi Authentication
            var multiAuthenticationConfigurations = _configuration.GetSection(WebHostAuthenticationOptions.MULTI_CONFIG_SECTION_NAME)
                .GetChildren().ToList();
            foreach (var section in multiAuthenticationConfigurations)
            {
                services.AddAuthentication()
                    .AddMicrosoftIdentityWebApp(section, section.Key, null);
            }

            //Setup Singular Authentication
            var authenticationConfiguration = _configuration.GetSection(WebHostAuthenticationOptions.SECTION_NAME)
                .Get<WebHostAuthenticationOptions>();
            if (multiAuthenticationConfigurations.Count == 0 && authenticationConfiguration != null)
            {
                services.AddMicrosoftIdentityWebApiAuthentication(_configuration, WebHostAuthenticationOptions.SECTION_NAME);
            }

            // Add controllers
            services
                .AddControllers(o =>
                {
                    if (multiAuthenticationConfigurations.Count != 0 || authenticationConfiguration != null)
                    {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        o.Filters.Add(new AuthorizeFilter(policy));
                    }
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                })
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(Startup).Assembly));

            // Add Swagger
            services.AddSwaggerGen(config =>
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

        /// <summary>
        /// Configures the application.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISystemContext systemContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webHostOptions = _configuration.GetSection(WebHostOptions.SECTION_NAME)
                .Get<WebHostOptions>();
            webHostOptions ??= new WebHostOptions();

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, request) =>
                {
                    const string prefixHeader = "X-Forwarded-Prefix";
                    if (!request.Headers.TryGetValue(prefixHeader, out var prefix))
                    {
                        return;
                    }

                    swaggerDoc.Servers = new List<OpenApiServer>()
                    {
                        new() { Url = prefix }
                    };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(webHostOptions.SwaggerEndpointUrl, webHostOptions.SwaggerEndpointName);
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
