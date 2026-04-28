using Newtonsoft.Json;

namespace Lab07_GeorgeGuerra.Controllers;

public partial class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Pasa la solicitud al siguiente middleware
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500; // Internal Server Error
            context.Response.ContentType = "application/json";
            var errorResponse = new { message = "Ocurrio un error interno", details = ex.Message };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse)); // Enviar la respuesta personalizada
        }
    }
}

