

using AutoMapper;


using Common.Extensions.HeaderUril;
using Common.Helper;
using Common.Helper.HttpResultHelper;
using Common.Services;
using Common.Services.Interfaces;
using Confluent.Kafka;
using Database.Models;
using External.Kafka.Implement;
using External.Kafka.Interface;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using PRJ_Project.API.HealthCheck;
using PRJ_Project.API.Middleware;
using PRJ_Project.API;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using APSharing.WebHttpClient;
using PRJ_Project.Service;
using Serilog;
using Serilog.Events;
using Serilog.Context;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Auth;


var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Environment SystemName: " + EnvironmentHelper.GetEnvironment("SystemName"));

var logger = new LoggerConfiguration()
 .MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
 .ReadFrom.Configuration(builder.Configuration)
 .Enrich.FromLogContext()
 .CreateLogger();
builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

// Add services to the container. 
builder.Services.AddTransient<IHeadersUtils, HeadersUtil>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<IHttpResultHelper, HttpResultHelper>();
builder.Services.AddTransient<IKakfaProducer, KafkaProducer>();


ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

//ServicesDependencyContainer.RegisterServices(builder.Services);
WebServicesDependencyContainer.RegisterServices(builder.Services);
ServicesDependencyContainer.RegisterServices(builder.Services);
LogActionFilterRegistration.RegisterServices(builder.Services);
//RepositoriesDependencyContainer.RegisterRepositories(builder.Services);

//builder.Services.AddScoped<IEntityUnitOfWork, EntityUnitOfWork>();
//builder.Services.AddDbContext<DbMasterTemplateApiContext>(options => options.UseSqlServer(EnvironmentHelper.GetEnvironment("DBConnectionString")));
builder.Services.AddDbContext<DbQueryContext>(options => options.UseSqlServer(EnvironmentHelper.GetEnvironment("DBConnectionString"), b => b.MigrationsAssembly("Database.Models")));
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(EnvironmentHelper.GetEnvironment("DBConnectionString"), b => b.MigrationsAssembly("Database.Models")));
//builder.Services.AddDbContextFactory<APEQuestionnaireContext>(options => options.UseSqlServer(EnvironmentHelper.GetEnvironment("DBConnectionString")));
builder.Services.AddHttpClient();



// builder.Services.AddDbContext<ApequestionnaireContext>(options => options.UseSqlServer(EnvironmentHelper.GetEnvironment("DBConnectionString")));
builder.Services.AddControllers()
   .AddNewtonsoftJson(opts =>
   {
       opts.SerializerSettings.Converters.Add(new StringEnumConverter());
   })
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
   });

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
       .AllowCredentials().Build();
    });
});

// builder.Services.AddHttpClient(Constants.IdentityClient, httpClient =>
// {
//     httpClient.BaseAddress = new Uri(EnvironmentHelper.GetEnvironment("IdentityBaseUrl"));
//     httpClient.DefaultRequestHeaders.Add("ClientId", EnvironmentHelper.GetEnvironment("ClientId"));
//     httpClient.DefaultRequestHeaders.Add("ClientSecret", EnvironmentHelper.GetEnvironment("ClientSecret"));
// });


builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

//validation model Request
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new BadRequestObjectResult(new
        {
            Message = "One or more validation errors occurred.",
            Data = context.ModelState.Values.SelectMany(x => x.Errors)
             .Select(x => x.ErrorMessage)
        });
    };
});

builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM After sale API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter the token with the `Bearer` prefix, e.g. \"Bearer abcde12345\".",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    option.SchemaFilter<EnumSchemaFilter>();
    option.CustomSchemaIds((type) =>
    {
        string defaultSchemaSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name;

            var prefix = modelType.GetGenericArguments()
                .Select(defaultSchemaSelector)
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }

        return defaultSchemaSelector(type);
    });
});

////#if !DEBUG
////var _env = new EnvironmentAuthenHelper();
////builder.Services.AddAuthentication("Bearer")

////.AddOAuth2Introspection("Bearer", options =>
////{
////    options.Authority = _env.Authority;
////    options.ClientId = _env.ApiName;
////    options.ClientSecret = _env.ApiSecret;
////    if (_env.IsDevelopment)
////    {
////        options.DiscoveryPolicy.RequireHttps = false;
////    }
////});



var _env = new EnvironmentAuthenHelper();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddOAuth2Introspection("OAuth2Introspection", options =>
    {
        options.Authority = _env.Authority;
        options.ClientId = _env.ApiName;
        options.ClientSecret = _env.ApiSecret;
        if (_env.IsDevelopment)
        {
            options.DiscoveryPolicy.RequireHttps = false;
        }
    });

builder.Services.AddJwt();
builder.Services.AddAuthorization(options =>
{
    // ใช้ Multi-Scheme Policy เพื่อรองรับทั้ง JWT และ OAuth2 Introspection
    options.AddPolicy("MultiSchemePolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "OAuth2Introspection");
        policy.RequireAuthenticatedUser();
    });
    options.AddPolicy("CRM.Read", policy => policy.RequireClaim("scope", "CRM.Read"));
    options.AddPolicy("CRM.Write", policy => policy.RequireClaim("scope", "CRM.Write"));
});
builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationMiddleware>();

//#endif

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddControllers().AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
});

// non reqired string model
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddSingleton<IProducer<string, string>>(sp =>
{
    var producerConfig = new ProducerConfig();

    producerConfig = new ProducerConfig();
    producerConfig.BootstrapServers = EnvironmentHelper.GetEnvironment("Kafka_Bootstrapservers");
    producerConfig.SaslUsername = EnvironmentHelper.GetEnvironment("Kafka_SaslUsername");
    producerConfig.SaslPassword = EnvironmentHelper.GetEnvironment("Kafka_SaslPassword");
    //producerConfig.SslCaPem = EnvironmentHelper.GetEnvironment("Kafka_SslCaPem");
    //producerConfig.SslCertificatePem = EnvironmentHelper.GetEnvironment("Kafka_SslCertificatePem");
    //producerConfig.SslKeyPem = EnvironmentHelper.GetEnvironment("Kafka_SslKeyPem");
    producerConfig.SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----";
    producerConfig.SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----";
    producerConfig.SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----";
    producerConfig.SecurityProtocol = SecurityProtocol.SaslSsl;
    producerConfig.SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None;
    producerConfig.SaslMechanism = SaslMechanism.ScramSha512;
    producerConfig.Partitioner = Confluent.Kafka.Partitioner.Random;

    producerConfig.Acks = Acks.None;
    producerConfig.LingerMs = 40;
    producerConfig.BatchSize = 3276000;
    //producerConfig.RequestTimeoutMs = 3000;
    //producerConfig.MessageTimeoutMs = 3000;

    return new ProducerBuilder<string, string>(producerConfig).Build();
});

//builder.Services.AddSingleton<IProducer<string, string>>(sp =>
//{
//    var producerConfig = new ProducerConfig
//    {
//        BootstrapServers = EnvironmentHelper.GetEnvironment("Kafka_Bootstrapservers"),
//        SaslUsername = EnvironmentHelper.GetEnvironment("Kafka_SaslUsername"),
//        SaslPassword = EnvironmentHelper.GetEnvironment("Kafka_SaslPassword"),
//        SslCaPem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCaPem") + "\n-----END CERTIFICATE-----",
//        SslCertificatePem = "-----BEGIN CERTIFICATE-----\n" + Environment.GetEnvironmentVariable("Kafka_SslCertificatePem") + "\n-----END CERTIFICATE-----",
//        SslKeyPem = "-----BEGIN PRIVATE KEY-----\n" + Environment.GetEnvironmentVariable("Kafka_SslKeyPem") + "\n-----END PRIVATE KEY-----",
//        SecurityProtocol = SecurityProtocol.SaslSsl,
//        SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
//        SaslMechanism = SaslMechanism.ScramSha512,
//        Partitioner = Confluent.Kafka.Partitioner.Random,

//        // Fire-and-Forget Configuration - ไม่รอ ACK เลย
//        Acks = Acks.None,

//        // Timeout settings - ป้องกันค้างถ้ามีปัญหา
//        RequestTimeoutMs = 3000,         // Timeout 3 วินาที
//        MessageTimeoutMs = 5000,         // Message timeout 5 วินาที
//        SocketTimeoutMs = 3000,          // Socket timeout 3 วินาที

//        // Performance settings
//        LingerMs = 10,                   // ลดจาก 40 เป็น 10 เพื่อส่งเร็วขึ้น
//        BatchSize = 1000000,             // ลดจาก 3.2MB เป็น 1 เพื่อไม่ให้รอนาน

//        // Connection settings
//        MaxInFlight = 10,                // อนุญาตส่ง request พร้อมกันได้ 10 request
//        EnableIdempotence = false,       // ปิดเพื่อ performance (เนื่องจาก Acks.None)

//        // Retry settings - ลด retry เพื่อไม่ให้ค้างนาน
//        MessageSendMaxRetries = 0        // ไม่ retry ถ้าส่งไม่สำเร็จ
//    };

//    return new ProducerBuilder<string, string>(producerConfig)
//        .SetErrorHandler((_, error) =>
//        {
//            // Log error แต่ไม่ throw exception
//            Console.WriteLine($"[Kafka Error] Code: {error.Code}, Reason: {error.Reason}");
//        })
//        .SetLogHandler((_, log) =>
//        {
//            // Log เฉพาะ error และ warning
//            if (log.Level <= SyslogLevel.Warning)
//            {
//                Console.WriteLine($"[Kafka Log] {log.Level}: {log.Message}");
//            }
//        })
//        .Build();
//});

builder.Services.AddHealthChecks()
    .AddCheck<SQLHealthCheck>(nameof(SQLHealthCheck));
builder.Services.AddSingleton<SQLHealthCheck>();
builder.Services.AddDataProtection();
//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    LogContext.PushProperty("Username", context?.Request?.Headers["EmpCode"].ToString() ?? "anonymous");
    LogContext.PushProperty("Apiname", context?.Request?.Path);
    await next();
});

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var basePath = "api/v1/apcrm-prj-project-service";
// Configure the HTTP request pipeline.
if (EnvironmentHelper.GetIsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = basePath + "/swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/{basePath}/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
            options.RoutePrefix = $"{basePath}/swagger";
        }
    });
}

app.UseRouting();
app.UseCors("EnableCORS");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<MainRoleHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks($"{basePath}/health-check", new HealthCheckOptions()
{
    ResultStatusCodes = {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
    Predicate = _ => true,
    ResponseWriter = WriteResponse
});

app.Run();

static Task WriteResponse(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new System.Text.Json.JsonWriterOptions { Indented = true };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new System.Text.Json.Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());
            jsonWriter.WriteString("description", healthReportEntry.Value.Description);
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    var result = memoryStream.ToArray();
    memoryStream.Dispose();

    if (healthReport.Status == HealthStatus.Unhealthy)
    {
        Console.WriteLine(Encoding.UTF8.GetString(result));
    }



    return context.Response.WriteAsync(
        Encoding.UTF8.GetString(result));
}