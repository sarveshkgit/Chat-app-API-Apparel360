using Apperel360.API.Hubs;
using Apperel360.Infrastructure.IoC;
using Microsoft.AspNetCore.Cors.Infrastructure;

var _corspolicy = "MasterAppCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices(builder.Configuration);// API Dependency
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();

// ADD CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(_corspolicy,
    policy =>
    {
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

    });
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
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");
app.Run();
