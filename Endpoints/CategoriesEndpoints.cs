using Gestor.API.Definitions;
using Gestor.API.DTOs.Request.Categoria;
using Gestor.API.Services;

namespace Gestor.API.Endpoints
{
    public class CategoriesEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            // Agrupar los endpoints
            var cateriasGroup = app.MapGroup("/api/categorias").WithTags("Categorías");
            // Endpoint para obtener todos las categorias
            cateriasGroup.MapGet("/", async (CategoriaServices categoriaService) =>
            {
                var categorias = await categoriaService.GetAllCategoriesAsync();
                return categorias.Any() ? Results.Ok(categorias) : Results.NotFound();
            });

            // Endpoint para obtener una categoria  por Id
            cateriasGroup.MapGet("/{id}", async (int id, CategoriaServices categoriaService) =>
            {
                var categoria = await categoriaService.GetCategoryByIdAsync(id);
                return categoria != null ? Results.Ok(categoria) : Results.NotFound();
            });

            // Endpoint para crear un nuevo registro
            cateriasGroup.MapPost("/", async (AddCategoriaRequest categoria, CategoriaServices categoriaService) =>
            {
                var creado = await categoriaService.CreateCategoryAsync(categoria);
                return Results.Created($"/api/categorias/{creado}", creado);
            });

            // Endpoint para actualizar una categoria existente
            cateriasGroup.MapPut("/{id}", async (int id, UpdateCategoriaRequest categoria, CategoriaServices categoriaService) =>
            {
                var actualizado = await categoriaService.UpdateCategoriaAsync(id, categoria);
                return actualizado == 1 ? Results.NoContent() : Results.NotFound();
            });

            // Endpoint para eliminar una categoria
            cateriasGroup.MapDelete("/{id}", async (int id, CategoriaServices categoriaService) =>
            {
                var eliminado = await categoriaService.DeleteCategoriaAsync(id);
                return eliminado ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
