using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using RolePolicy.Application;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.Persistence;
using RolePolicy.WebApi;
using RolePolicy.WebApi.Common.ActionProfile;
using RolePolicy.WebApi.Common.ResourceProfile;
using RolePolicy.WebApi.Common.RoleAccessProfile;
using RolePolicy.WebApi.Common.RoleProfile;
using RolePolicy.WebApi.Common.RoleResourceActionProfile;
using RolePolicy.WebApi.Common.UserProfile;
using RolePolicy.WebApi.Middlewares;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .CreateLogger();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(AppDomain.CurrentDomain.GetAssemblies()));
    config.AddProfile(typeof(ActionProfile));
    config.AddProfile(typeof(ResourceProfile));
    config.AddProfile(typeof(UserProfile));
    config.AddProfile(typeof(RoleProfile));
    config.AddProfile(typeof(RoleAccessProfile));
    config.AddProfile(typeof(RoleResourceActionProfile));
});

builder.Services.AddApplication();
/*builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddDbContext<QueuesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddHttpContextAccessor();
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
//
//builder.Services.AddHttpContextAccessor();
//builder.Host.UseSerilog((context, config) =>
//{
//    config.MinimumLevel.Information()
//    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
//    .Enrich.WithCorrelationId()
//    .WriteTo.Debug(outputTemplate: "[{Timestamp:yyyy-MM-ddd HH:mm:ss.fff} {CorrelstionId} {Level:u3}] {Message.lj}{NewLine}{exception}")
//    .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-ddd HH:mm:ss.fff} {CorrelstionId} {Level:u3}] {Message.lj}{NewLine}{exception}")
//    .WriteTo.Seq("http://localhost:5341");

//});
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});
builder.Services.AddVersionedApiExplorer(options =>
                options.GroupNameFormat = "'v'VVV");
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
        ConfigureSwaggerOptions>();
builder.Services.AddApiVersioning();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        /*        var context = serviceProvider.GetRequiredService<QueuesDbContext>();
                DbInitializer.Initialize(context);*/
    }
    catch (Exception ex)
    {
        //Log.Fatal(ex, "An error occurred while initializing the database");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


    foreach (var description in provider.ApiVersionDescriptions)
    {
        config.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                 description.GroupName.ToUpperInvariant());
        config.RoutePrefix = string.Empty;

    }
});

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseApiVersioning();
//
app.UseSerilogRequestLogging();
//
app.MapControllers();



Log.Information("Приложение запущено");
app.Run();