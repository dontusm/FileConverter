using FileConverter.Api;
using FileConverter.Api.Middleware;
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
    app.UseOpenApi();
    app.UseSwaggerUi(); 
}

app.UseExceptionHandler("/error"); 
app.UseMiddleware<ExceptionHandlingMiddleware>(); 

app.MapControllers();
app.Run();
