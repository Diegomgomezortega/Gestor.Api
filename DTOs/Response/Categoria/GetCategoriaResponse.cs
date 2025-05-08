namespace Gestor.API.DTOs.Response.Categoria
{
    public record GetCategoriaResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; } = null!;
        public static GetCategoriaResponse ConvertFromEntity(Models.Categoria categoria)
        {
            return new GetCategoriaResponse()
            {
                Id = categoria.IdCategoria,
                Descripcion = categoria.Descripcion,
                Nombre = categoria.Nombre
            };
        }
    }
}
