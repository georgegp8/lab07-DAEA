using Lab07_GeorgeGuerra.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab07_GeorgeGuerra.Controllers;

[ApiController]
[Route("api/products")]

public class ProductController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateProduct([FromBody] CreateProductDto product, [FromHeader(Name = "role")] string role)
    {
        throw new Exception("Error de prueba");
        //return Ok(new { message = "Producto creado exitosamente." });
    }
}