namespace Gestor.API.DTOs.Request.Categoria
{
    public record UpdateCategoriaRequest
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; }=null!;
    }
}
