using Gestor.API.Definitions;
using Gestor.API.DTOs.Request.Producto;
using Gestor.API.Services;

namespace Gestor.API.Endpoints
{
    public class ProductsEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            // Agrupar los endpoints
            var productosGroup = app.MapGroup("/api/productos").WithTags("Productos");
            // Endpoint para obtener todos los productos
            productosGroup.MapGet("/", async (ProductoService productService) =>
            {
                var productos = await productService.GetAllProductsAsync();
                return productos.Any() ? Results.Ok(productos) : Results.NotFound();
            });

            // Endpoint para obtener un producto por Id
            productosGroup.MapGet("/{id}", async (int id, ProductoService productService) =>
            {
                var producto = await productService.GetProductByIdAsync(id);
                return producto != null ? Results.Ok(producto) : Results.NotFound();
            });

            // Endpoint para crear un nuevo producto
            productosGroup.MapPost("/", async (AddProductRequest producto, ProductoService productService) =>
            {
                var creado = await productService.CreateProductAsync(producto);
                return Results.Created($"/api/productos/{creado}", creado);
            });

            // Endpoint para actualizar un producto existente
            productosGroup.MapPut("/{id}", async (int id, UpdateProductoRequest producto, ProductoService productService) =>
            {
                var actualizado = await productService.UpdateProductAsync(id, producto);
                return actualizado == 1 ? Results.NoContent() : Results.NotFound();
            });

            // Endpoint para eliminar un producto
            productosGroup.MapDelete("/{id}", async (int id, ProductoService productService) =>
            {
                var eliminado = await productService.DeleteProductAsync(id);
                return eliminado ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
