using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using TicketManagement.EventManagementAPI.JwtTokenAuth;
using UserApiClientGenerated;

namespace TicketManagement.EventManagementAPI
{
    public class Startup
    {
        ////private static readonly Lazy<string> XmlCommentsFilePathLazy = new Lazy<string>(XmlDocumentationPath(typeof(Startup)));

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ////services.AddHealthChecks().AddCheck<UserApiHealthcheck>("user_api_check", tags: new[] { "ready" });
            services.AddOptions().Configure<UserApiOptions>(binder => binder.UserApiAddress = _configuration["UserApiAddress"]);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(JwtAutheticationConstants.SchemeName)
                .AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>(JwtAutheticationConstants.SchemeName, null);

            services.AddHttpClient<UsersApiClient>((provider, client) =>
            {
                var userApiAddress = provider.GetService<IOptions<UserApiOptions>>()?.Value.UserApiAddress;
                client.BaseAddress = new Uri(userApiAddress ?? string.Empty);
            });

            services.AddRepositories(_configuration.GetConnectionString("DefaultConnection"));
            services.AddControllers();
            ////services.AddSwaggerGen(options =>
            ////{
            ////    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Internal lab Demo 2", Version = "v1" });
            ////    options.IncludeXmlComments(XmlCommentsFilePathLazy.Value);
            ////    var jwtSecurityScheme = new OpenApiSecurityScheme
            ////    {
            ////        Description = "Jwt Token is required to access the endpoints",
            ////        In = ParameterLocation.Header,
            ////        Name = "JWT Authentication",
            ////        Type = SecuritySchemeType.Http,
            ////        Scheme = "bearer",
            ////        BearerFormat = "JWT",
            ////        Reference = new OpenApiReference
            ////        {
            ////            Id = JwtBearerDefaults.AuthenticationScheme,
            ////            Type = ReferenceType.SecurityScheme,
            ////        },
            ////    };

            ////    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
            ////    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            ////    {
            ////        { jwtSecurityScheme, Array.Empty<string>() },
            ////    });
            ////});

            services.AddOpenApiDocument();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
            ////app.UseSwagger();
            ////app.UseSwaggerUI(options =>
            ////{
            ////    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer API v1");
            ////});

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                ////endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                ////{
                ////    Predicate = check => check.Tags.Contains("ready"),
                ////}).WithMetadata(new AllowAnonymousAttribute());
                ////endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                ////{
                ////    Predicate = _ => false,
                ////}).WithMetadata(new AllowAnonymousAttribute());
            });
        }

        ////private static string XmlDocumentationPath(Type currentClass)
        ////{
        ////    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
        ////    var fileName = currentClass.GetTypeInfo().Assembly.GetName().Name + ".xml";
        ////    return Path.Combine(basePath, fileName);
        ////}
    }
}
