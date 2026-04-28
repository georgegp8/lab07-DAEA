using Lab07_GeorgeGuerra.Controllers;
using Microsoft.OpenApi.Models;
using ParameterValidationMiddleware = Lab07_GeorgeGuerra.Middlewares.ParameterValidationMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
    c.AddSecurityDefinition("X-Role", new OpenApiSecurityScheme
    {
        Name = "X-Role",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Header para el rol (ejemplo: Admin)."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-Role"
                }
            },
            Array.Empty<string>()
        }
    });
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
app.UseRouting();
app.UseMiddleware<ParameterValidationMiddleware>();
app.UseMiddleware<RoleBasedAccessMiddleware>();

app.MapControllers();

app.Run();