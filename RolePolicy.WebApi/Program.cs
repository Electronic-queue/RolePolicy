using RolePolicy.Application;
using RolePolicy.Persistence;
using Microsoft.Extensions.Options;
using RolePolicy.Application.Common.Mappings;
using RolePolicy.WebApi.Common.ActionProfile;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using RolePolicy.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(AppDomain.CurrentDomain.GetAssemblies()));
    config.AddProfile(typeof(ActionProfile));
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
builder.Services.AddControllers();

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
//app.UseMiddleware<CustomExceptionHandlerMiddleware>();

//app.UseMiddleware<CorrelationIdMiddleware>();
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
//app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseApiVersioning();
app.MapControllers();

//Log.Information("Приложение запущено");
app.Run();