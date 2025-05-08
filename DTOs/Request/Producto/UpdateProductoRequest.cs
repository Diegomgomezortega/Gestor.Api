namespace Gestor.API.DTOs.Request.Producto
{
    public record UpdateProductoRequest
    {
        public decimal Precio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdCategoria { get; set; }
        public string PaisFabricacion { get; set; } = null!;
    }
}
