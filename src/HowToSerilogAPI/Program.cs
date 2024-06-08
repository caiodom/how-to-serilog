

using Serilog;





var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
          .AddJsonFile($"appsettigs.json", true, true)
          .AddJsonFile($"appsettigs.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
          .AddEnvironmentVariables();
});

Log.Logger = new LoggerConfiguration()
                .ReadFrom
                   .Configuration(builder.Configuration)
                   .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

try
{
    Log.Information("API initializing....");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application encountered an error while starting.");
}
finally
{
    Log.CloseAndFlush();
}

