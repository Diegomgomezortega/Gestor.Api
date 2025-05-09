using Gestor.API.DTOs.Request.Producto;
using Gestor.API.DTOs.Response.Producto;
using Gestor.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor.API.Services
{
    public class ProductoService
    {
        private readonly GestorContext _context;

        public ProductoService(GestorContext context)
        {
            _context = context;
        }
        public async Task<int> CreateProductAsync(AddProductRequest request, CancellationToken cancellationToken)
        {
            var producto = request.ConvertToEntity();
            // Añadimos el producto al contexto
            _context.Productos.Add(producto);
            // Guardamos los cambios
            await _context.SaveChangesAsync(cancellationToken);
            return producto.IdProducto; 
        }
        public async Task<GetProductResponse> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity= await _context.Productos.Include(p=>p.Categoria).FirstOrDefaultAsync(p => p.IdProducto == id,cancellationToken);
            if (entity == null)
            {
                return null;
            }
            var response = GetProductResponse.ConvertFromEntity(entity);
            return response;
        }
        public async Task<List<GetProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var entities= await _context.Productos.Include(p=>p.Categoria).ToListAsync(cancellationToken);
            var response = entities.Select(e => GetProductResponse.ConvertFromEntity(e));
            return response.ToList();
        }
        public async Task<int> UpdateProductAsync(int id, UpdateProductoRequest updatedProduct, CancellationToken cancellationToken)
        {
            var entity = await _context.Productos.FindAsync(id,cancellationToken); 
            if (entity == null)
            {
                return 0;  //Si no existe la entidad, retorno 0
            }

            // Si existe, actualizamos los valores
            entity.Nombre = updatedProduct.Nombre;
            entity.Descripcion = updatedProduct.Descripcion;
            entity.Precio = updatedProduct.Precio;
            entity.PaisFabricacion = updatedProduct.PaisFabricacion;
            entity.IdCategoria = updatedProduct.IdCategoria;

            var result =await _context.SaveChangesAsync(cancellationToken);
            return result; //Retorno 1 si la entity fue modificada
        }
        public async Task<bool> DeleteProductAsync(int id,CancellationToken cancellationToken)
        {
            var entity = await _context.Productos.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                return false; // Si no se encuentra, devolvemos false
            }

            _context.Productos.Remove(entity); // Eliminamos el producto
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
