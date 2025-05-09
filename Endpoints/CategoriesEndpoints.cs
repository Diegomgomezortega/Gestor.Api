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
            cateriasGroup.MapGet("/", async (CategoriaServices categoriaService,CancellationToken cancellationToken) =>
            {
                var categorias = await categoriaService.GetAllCategoriesAsync(cancellationToken);
                return categorias.Any() ? Results.Ok(categorias) : Results.NotFound();
            });

            // Endpoint para obtener una categoria  por Id
            cateriasGroup.MapGet("/{id}", async (int id, CategoriaServices categoriaService,CancellationToken cancellationToken) =>
            {
                var categoria = await categoriaService.GetCategoryByIdAsync(id, cancellationToken);
                return categoria != null ? Results.Ok(categoria) : Results.NotFound();
            });

            // Endpoint para crear un nuevo registro
            cateriasGroup.MapPost("/", async (AddCategoriaRequest categoria, CategoriaServices categoriaService,CancellationToken cancellationToken) =>
            {
                var creado = await categoriaService.CreateCategoryAsync(categoria, cancellationToken);
                return Results.Created($"/api/categorias/{creado}", creado);
            });

            // Endpoint para actualizar una categoria existente
            cateriasGroup.MapPut("/{id}", async (int id, UpdateCategoriaRequest categoria, CategoriaServices categoriaService,CancellationToken cancellationToken) =>
            {
                var actualizado = await categoriaService.UpdateCategoriaAsync(id, categoria,cancellationToken);
                return actualizado == 1 ? Results.NoContent() : Results.NotFound();
            });

            // Endpoint para eliminar una categoria
            cateriasGroup.MapDelete("/{id}", async (int id, CategoriaServices categoriaService,CancellationToken cancellationToken) =>
            {
                var eliminado = await categoriaService.DeleteCategoriaAsync(id, cancellationToken);
                return eliminado ? Results.NoContent() : Results.BadRequest();
            });
        }
    }
}
