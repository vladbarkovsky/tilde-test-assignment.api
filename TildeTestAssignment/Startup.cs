using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using TildeTestAssignment.Application.Common;
using TildeTestAssignment.ORM.Services;
using TildeTestAssignment.ORM.Services.Interfaces;
using TildeTestAssignment.Web;
using TildeTestAssignment.Web.ApiSpecification;

namespace TildeTestAssignment
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString(AppSettings.ConnectionStrings.Development));

                // Throw exceptions in case of performance issues with single queries.
                // See https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries.
                options.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.Scan(scan => scan.FromAssemblyOf<Program>()
                .AddClasses(x => x.AssignableTo<ISingletonService>())
                .AsSelfWithInterfaces()
                .WithSingletonLifetime()
                .AddClasses(x => x.AssignableTo<IScopedService>())
                .AsSelfWithInterfaces()
                .WithScopedLifetime()
                .AddClasses(x => x.AssignableTo<ITransientService>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    // NOTE: CORS allows simple methods (GET, HEAD, POST) regardless of
                    // Access-Control-Allow-Methods header content.
                    // Source: https://stackoverflow.com/a/44385327.

                    builder
                        .WithOrigins(_configuration[AppSettings.ClientBaseUrl])
                        .WithHeaders(HeaderNames.ContentType)
                        .Build();
                });
            });

            if (_webHostEnvironment.IsDevelopment())
            {
                services.AddOpenApiDocument(settings =>
                {
                    settings.Title = "Tilde test assignment API specification";

                    // Overriding default name generation patterns.
                    settings.SchemaSettings.SchemaNameGenerator = new SchemaNameGenerator();
                });
            }

            services.AddExceptionHandler(options =>
            {
                options.AllowStatusCode404Response = true;

                options.ExceptionHandler = async httpContext =>
                {
                    var exception = httpContext.Features.Get<IExceptionHandlerFeature>().Error;

                    if (exception is HttpStatusException)
                    {
                        httpContext.Response.StatusCode = (exception as HttpStatusException).StatusCode;
                        httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                        await httpContext.Response.WriteAsync(exception.Message);
                    }
                };
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();

            if (_webHostEnvironment.IsDevelopment())
            {
                app.UseSwaggerUi(settings =>
                {
                    settings.Path = "/api";
                    settings.DocumentPath = "/api/specification.json";
                });
            }

            app.UseExceptionHandler();

            // Enable HTTP routing.
            app.UseRouting();
            // Enable internet access to wwwroot.
            app.UseStaticFiles();
            // Configure HTTP endpoints for controllers
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}