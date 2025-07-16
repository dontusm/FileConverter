using FileConverter.Api;
using FileConverter.Application;
using FileConverter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()              
    .AddInfrastructure()
    .AddApi(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
