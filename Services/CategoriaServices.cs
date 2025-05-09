using Gestor.API.DTOs.Request.Categoria;
using Gestor.API.DTOs.Response.Categoria;
using Gestor.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor.API.Services
{
    public class CategoriaServices
    {
        private readonly GestorContext _context;

        public CategoriaServices(GestorContext context)
        {
            _context = context;
        }
        public async Task<int> CreateCategoryAsync(AddCategoriaRequest request, CancellationToken cancellationToken)
        {
            var entity = request.ConvertToEntity();
            // Añadimos el registro al contexto
            _context.Categoria.Add(entity);
            // Guardamos los cambios
            await _context.SaveChangesAsync(cancellationToken);
            return entity.IdCategoria;
        }
        public async Task<GetCategoriaResponse> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Categoria.FirstOrDefaultAsync(p => p.IdCategoria == id,cancellationToken);
            if (entity == null)
            {
                return null;
            }
            var response = GetCategoriaResponse.ConvertFromEntity(entity);
            return response;
        }
        public async Task<List<GetCategoriaResponse>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var entities = await _context.Categoria.ToListAsync(cancellationToken);
            var response = entities.Select(e => GetCategoriaResponse.ConvertFromEntity(e));
            return response.ToList();
        }
        public async Task<int> UpdateCategoriaAsync(int id, UpdateCategoriaRequest updatedProduct, CancellationToken cancellationToken)
        {
            var entity = await _context.Categoria.FindAsync(id,cancellationToken);
            if (entity == null)
            {
                return 0;  //Si no existe la entidad, retorno 0
            }

            // Si existe, actualizamos los valores
            entity.Nombre = updatedProduct.Nombre;
            entity.Descripcion = updatedProduct.Descripcion;

            var result = await _context.SaveChangesAsync(cancellationToken);
            return result; //Retorno 1 si la entity fue modificada
        }
        public async Task<bool> DeleteCategoriaAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Categoria.FindAsync(id,cancellationToken);
                if (entity == null)
                {
                    return false; // Si no se encuentra, devolvemos false
                }

                _context.Categoria.Remove(entity); // Eliminamos el registro
                var result = await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
