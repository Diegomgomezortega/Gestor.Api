using Gestor.API.Models;
using System.Net.NetworkInformation;

namespace Gestor.API.DTOs.Response.Producto
{
    public record GetProductResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; } = "";
        public string Categoria { get; set; } = null!;
        public decimal Precio { get; set; }
        public string PaisFabricacion { get; set; } = null!;
        public int IdCategoria { get; set; }

        public static GetProductResponse ConvertFromEntity(Models.Producto producto)
        {
            return new GetProductResponse()
            {
                Id= producto.IdProducto,
                Nombre= producto.Nombre,
                Precio= producto.Precio,
                Descripcion= producto.Descripcion,
                Categoria= producto.Categoria.Nombre,
                PaisFabricacion= producto.PaisFabricacion,
                IdCategoria=producto.IdCategoria
                
            };
        }
    }
}
