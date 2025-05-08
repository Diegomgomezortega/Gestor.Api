using Gestor.API.Models;

namespace Gestor.API.DTOs.Request.Categoria
{
    public record AddCategoriaRequest
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; }=null!;
        public Models.Categoria ConvertToEntity()
        {
            return new Models.Categoria()
            {
                Nombre = Nombre,
                Descripcion = Descripcion
            };

        }
    }
}
