using Apperel360.API.Hubs;
using Apperel360.Infrastructure.IoC;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var _corspolicy = "MasterAppCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices(builder.Configuration);// API Dependency
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ADD CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(_corspolicy,
    policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true);

    });
});



builder.Services.AddSignalR();


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// user cors
app.UseCors(_corspolicy);

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(); // already there for wwwroot
// To Save and fetch Images
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new PathString("/Images")
    //FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "Images")),
    //RequestPath = "/Images"
});

// Serve files from ProfileImages folder (outside wwwroot)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ProfileImages")),
    RequestPath = new PathString("/ProfileImages")
});

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");
app.Run();
