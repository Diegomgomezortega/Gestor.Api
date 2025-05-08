using Gestor.API.Models;

namespace Gestor.API.DTOs.Request.Producto
{
    public record AddProductRequest
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string PaisFabricacion { get; set; } = null!;
        public Models.Producto ConvertToEntity()
        {
            return new Models.Producto()
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Precio = Precio,
                IdCategoria = IdCategoria,
                PaisFabricacion = PaisFabricacion

            };

        }
    }
}
