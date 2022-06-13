using System.Reflection;
using AutoMapper;
using FluentValidation;
using Io.TedTalk.Services.Repositories;
using Io.TedTalk.Services.Repositories.Implementations;
using Io.TedTalk.Services.Services;
using IO.TedTalk.Api.Framrwork.AspNetCore.Config;
using IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.ExceptionHandling;
using IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results;
using IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Results.Wrapping;
using IO.TedTalk.Api.Framrwork.Models;
using IO.TedTalk.Core;
using IO.TedTalk.Data;
using IO.TedTalk.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace IO.TedTalk.Api;

public class Startup
{

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine(Environment.EnvironmentName);

        // ASP.NET Core & 3rd parties
        services.AddControllers(options =>
        {
            options.Filters.AddService(typeof(GlobalExceptionFilter));
            options.Filters.AddService(typeof(ResultFilter));
        });
        services.AddCors();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(DefaultMappingProfile).Assembly);
        services.AddHealthChecks();

        services.AddHttpContextAccessor();

        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        //Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        // Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(AppConsts.ApiVersion, new() { Title = AppConsts.ApiTitle, Version = AppConsts.ApiVersion });
            //uncomment for v2
            //options.SwaggerDoc("v2", new() { Title = AppConsts.ApiTitle, Version = "v2" });

            options.AddSecurityDefinition("Bearer", new()
            {
                Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });

        //db context
        services.AddDbContext<IODbContext>(options =>
            options.UseSqlite($"Data Source={AppConsts.Database.DbPath}"));


        // Configure options
        services.Configure<Settings>(Configuration);

        //Setting up framework
        //Adds services required for using options.
        services.AddOptions();
        services.AddCors();

        // Add Framework to DI
        services.AddTransient<ResultFilter>();
        services.AddTransient<IActionResultWrapperFactory, ActionResultWrapperFactory>();
        services.AddTransient<IAspnetCoreConfiguration, AspnetCoreConfiguration>();
        services.AddTransient<IErrorInfoBuilder, ErrorInfoBuilder>();
        services.AddTransient<GlobalExceptionFilter>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSingleton(Configuration);


        //Register Services in DI
        services
            .AddTransient<TedService>()
            .AddTransient<ITedRepository, TedRepository>();



    }

    public void Configure(IApplicationBuilder app,
        IWebHostEnvironment env,
        IMapper mapper,
        IApiVersionDescriptionProvider provider)
    {

        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseRouting();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                        [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });

        });

        if (env.IsDevelopment())
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

    }
}
