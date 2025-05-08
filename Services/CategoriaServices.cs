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
        public async Task<int> CreateCategoryAsync(AddCategoriaRequest request)
        {
            var entity = request.ConvertToEntity();
            // Añadimos el registro al contexto
            _context.Categoria.Add(entity);
            // Guardamos los cambios
            await _context.SaveChangesAsync();
            return entity.IdCategoria;
        }
        public async Task<GetCategoriaResponse> GetCategoryByIdAsync(int id)
        {
            var entity = await _context.Categoria.FirstOrDefaultAsync(p => p.IdCategoria == id);
            if (entity == null)
            {
                return null;
            }
            var response = GetCategoriaResponse.ConvertFromEntity(entity);
            return response;
        }
        public async Task<List<GetCategoriaResponse>> GetAllCategoriesAsync()
        {
            var entities = await _context.Categoria.ToListAsync();
            var response = entities.Select(e => GetCategoriaResponse.ConvertFromEntity(e));
            return response.ToList();
        }
        public async Task<int> UpdateCategoriaAsync(int id, UpdateCategoriaRequest updatedProduct)
        {
            var entity = await _context.Categoria.FindAsync(id);
            if (entity == null)
            {
                return 0;  //Si no existe la entidad, retorno 0
            }

            // Si existe, actualizamos los valores
            entity.Nombre = updatedProduct.Nombre;
            entity.Descripcion = updatedProduct.Descripcion;

            var result = await _context.SaveChangesAsync();
            return result; //Retorno 1 si la entity fue modificada
        }
        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var entity = await _context.Categoria.FindAsync(id);
            if (entity == null)
            {
                return false; // Si no se encuentra, devolvemos false
            }

            _context.Categoria.Remove(entity); // Eliminamos el registro
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
