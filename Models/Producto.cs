namespace Gestor.API.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int IdCategoria { get; set; }

    public string PaisFabricacion { get; set; } = null!;

    public virtual Categoria Categoria { get; set; } = null!;
}
