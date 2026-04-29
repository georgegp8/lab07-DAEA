using Lab07_GeorgeGuerra.Controllers;
using Lab07_GeorgeGuerra.Middlewares;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseMiddleware<ParameterValidationMiddleware>();
//app.UseMiddleware<RoleBasedAccessMiddleware>();

app.MapControllers();

app.Run();